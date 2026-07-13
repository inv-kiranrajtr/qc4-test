using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using log4net.Appender;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// QCWebMinimalLock の概要の説明です。
    /// 参考サイト:http://d.hatena.ne.jp/m-tanaka/searchdiary?word=%2a%5blog4net%5d
    /// </summary>
    public class QCWebMinimalLock : FileAppender.MinimalLock {
        private string m_filename;
        private bool m_append;
        private Stream m_stream = null;
        private ConcurrentStream c_stream = null;

        /// <summary>
        /// Prepares to open the file when the first message is logged.
        /// </summary>
        /// <param name="filename">The filename to use</param>
        /// <param name="append">Whether to append to the file, or overwrite</param>
        /// <param name="encoding">The encoding to use</param>
        /// <remarks>
        /// <para>
        /// Open the file specified and prepare for logging. 
        /// No writes will be made until <see cref="AcquireLock"/> is called.
        /// Must be called before any calls to <see cref="AcquireLock"/>,
        /// <see cref="ReleaseLock"/> and <see cref="CloseFile"/>.
        /// </para>
        /// </remarks>
        public override void OpenFile(string filename, bool append, Encoding encoding) {
            m_filename = filename;
            m_append = append;
        }

        /// <summary>
        /// Close the file
        /// </summary>
        /// <remarks>
        /// <para>
        /// Close the file. No further writes will be made.
        /// </para>
        /// </remarks>
        public override void CloseFile() {
            // NOP
        }

        /// <summary>
        /// Acquire the lock on the file
        /// </summary>
        /// <returns>A stream that is ready to be written to.</returns>
        /// <remarks>
        /// <para>
        /// Acquire the lock on the file in preparation for writing to it. 
        /// Return a stream pointing to the file. <see cref="ReleaseLock"/>
        /// must be called to release the lock on the output file.
        /// </para>
        /// </remarks>
        public override Stream AcquireLock() {
            if (m_stream == null) {
                try {
                    using (CurrentAppender.SecurityContext.Impersonate(this)) {
                        // Ensure that the directory structure exists
                        string directoryFullName = Path.GetDirectoryName(m_filename);

                        // Only create the directory if it does not exist
                        // doing this check here resolves some permissions failures
                        if (!Directory.Exists(directoryFullName)) {
                            Directory.CreateDirectory(directoryFullName);
                        }

                        if (c_stream == null) {
                            c_stream = ConcurrentStream.GetInstance(m_filename, m_append, FileAccess.Write, FileShare.Read);
                        }
                        m_stream = c_stream;
                        m_append = true;
                    }
                } catch (Exception e1) {
                    CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + m_filename + ". " + e1.Message);
                }
            }
            return m_stream;
        }

        /// <summary>
        /// Release the lock on the file
        /// </summary>
        /// <remarks>
        /// <para>
        /// Release the lock on the file. No further writes will be made to the 
        /// stream until <see cref="AcquireLock"/> is called again.
        /// </para>
        /// </remarks>
        public override void ReleaseLock() {
            using (CurrentAppender.SecurityContext.Impersonate(this)) {
                m_stream.Close();
                m_stream = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConcurrentStream : Stream {
        private string path;
        private bool append;
        private FileAccess access;
        private FileShare share;

        private QueueManager queueManager;
        private static ConcurrentStream instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        public static ConcurrentStream GetInstance(string path, bool append, FileAccess access, FileShare share) {
            if (instance == null) {
                instance = new ConcurrentStream(path, append, access, share);
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        private ConcurrentStream(string path, bool append, FileAccess access, FileShare share ) {
            this.path = path;
            this.append = append;
            this.access = access;
            this.share = share;
            this.queueManager = QueueManager.GetInstance(path, append, access, share);
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override long Length {
            get { return 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override long Position {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count) {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin) {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value) {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Flush() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count) {
            CachedEntry entry = new CachedEntry(buffer, offset, count);
            queueManager.Enqueue(entry);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class QueueManager {

        private string path;
        private bool append;
        private FileAccess access;
        private FileShare share;

        private Queue syncQueue = Queue.Synchronized(new Queue());

        private bool running = false;
        private Random rnd = new Random();
        private DateTime retryTime = DateTime.MaxValue;

        private static TimeSpan RETRY_MAX_SPAN = TimeSpan.FromMinutes(1);
        private static QueueManager instance;

        private const int MAX_BATCH_SIZE = 100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        public static QueueManager GetInstance(string path, bool append, FileAccess access, FileShare share) {
            if (instance == null) {
                instance = new QueueManager(path, append, access, share);
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        private QueueManager(string path, bool append, FileAccess access, FileShare share) {
            this.path = path;
            this.append = append;
            this.access = access;
            this.share = share;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        internal void Enqueue(CachedEntry entry) {
            syncQueue.Enqueue(entry);

            if (!running) {
                lock (this) {
                    running = true;
                    Thread th = new Thread(new ThreadStart(this.Dequeue));
                    th.Start();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Dequeue() {
            CachedEntry entry = null;

            try {
                using (FileStream fs = new FileStream(path, FileMode.Append, access, share)) {
                    int processedCount = 0;
                    while (true) {
                        processedCount++;
                        if (syncQueue.Count == 0) {	//queueの中身が存在しない場合は、終了
                            lock (this) {
                                running = false;
                                return;
                            }
                        } else {
                            entry = (CachedEntry)syncQueue.Dequeue();
                        }

                        if (entry != null) {
                            Write(entry, fs);
                        }
                    }
                }
            } catch (IOException) {
                if (DateTime.Now - retryTime > RETRY_MAX_SPAN) {
                    lock (this) {
                        running = false;
                    }
                    throw;
                }
                //Random時間待機した後にリトライ
                Thread.Sleep(rnd.Next(1000));
                Console.WriteLine("Retry:" + DateTime.Now);
                retryTime = DateTime.Now;
                Dequeue();
            }
        }

        private void Write(CachedEntry entry, FileStream fs) {
            fs.Write(entry.Buffer, entry.Offset, entry.Count);
            fs.Flush();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CachedEntry {
        private byte[] buffer;
        private int offset;
        private int count;

        internal byte[] Buffer {
            get { return buffer; }
        }

        internal int Offset {
            get { return offset; }
        }

        internal int Count {
            get { return count; }
        }

        internal CachedEntry(byte[] buffer, int offset, int count) {
            this.buffer = new byte[buffer.Length];
            buffer.CopyTo(this.buffer, 0);
            this.offset = offset;
            this.count = count;
        }
    }
}
