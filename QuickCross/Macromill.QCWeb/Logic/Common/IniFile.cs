#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IniFile.cs
 * バージョン：1.0.0
 * 概　　　要：バッチINIファイル読み込みクラス
 * 作　成　日：2012/03/12
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
#define FOR_UNIT_TEST
#if FOR_UNIT_TEST
using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// INIファイルを扱う静的クラス
    /// </summary>
    [ComVisible(false), Guid("CB085A2A-74D8-4B37-82F5-FADF9C5D6D32")]
    public static class IniFile
    {
        #region Win32API
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSectionNames(
                                    byte[] lpszReturnBuffer
                                  , int nSize
                                  , string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(
                                    string lpAppName
                                  , byte[] lpReturnedString
                                  , int nSize
                                  , string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
                                    string lpApplicationName
                                  , string lpKeyName
                                  , string lpDefault
                                  , StringBuilder lpReturnedString
                                  , int nSize
                                  , string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
                                    string lpApplicationName
                                  , string lpKeyName
                                  , string lpDefault
                                  , byte[] lpReturnedString
                                  , int nSize
                                  , string lpFileName);

        [DllImport("kernel32.DLL")]
        private static extern int GetPrivateProfileInt(
                                    string lpApplicationName
                                  , string lpKeyName
                                  , int nDefault
                                  , string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
                                    string lpApplicationName
                                  , string lpKeyName
                                  , string lpString
                                  , string lpFileName);
        #endregion

        private static string GetIniFile()
        {
            //return System.IO.Path.Combine(
            //    /*System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)*/
            //    /*System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)*/
            //    @"c:\qcweb\", "QCWebConfig.ini");
            return System.IO.Path.Combine(
                System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                , @"Q4\test\DB.ini");
        }

        /// <summary>
        /// 定義ファイル内のセクション一覧を配列で取得する
        /// </summary>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <returns>定義ファイル内のすべてのセクション文字列からなる配列</returns>
        public static string[] GetSections(string inifilepath = null)
        {
            if (string.IsNullOrWhiteSpace(inifilepath)) inifilepath = GetIniFile();
            byte[] byteBuf = new byte[65536];
            int size = GetPrivateProfileSectionNames(byteBuf, byteBuf.Length, inifilepath);
            if (size == 0) return null;
            string buf = Encoding.Default.GetString(byteBuf, 0, size - 1);
            return buf.Split((char)0);
        }

        /// <summary>
        /// 定義ファイルの特定のセクション内のキー一覧を配列で取得する
        /// </summary>
        /// <param name="section">取得するセクション名</param>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <returns><paramref name="section"/>に指定したセクションに含まれるキー文字列からなる配列</returns>
        public static string[] GetKeys(string section, string inifilepath = null)
        {
            if (string.IsNullOrWhiteSpace(inifilepath)) inifilepath = GetIniFile();
            byte[] byteBuf = new byte[65536];
            // int size = GetPrivateProfileSection(section, byteBuf, byteBuf.Length, inifilepath);
            int size = GetPrivateProfileString(section, null, null, byteBuf, byteBuf.Length, inifilepath);
            if (size == 0) return null;
            string buf = Encoding.Default.GetString(byteBuf, 0, size - 1);
            string[] res = buf.Split((char)0);
            return res;
        }

        /// <summary>
        /// 定義ファイルから文字列を取得する
        /// </summary>
        /// <param name="section">取得するセクション名</param>
        /// <param name="key">取得するキー名</param>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <param name="value">取得できない場合に返すデフォルト値 (省略可、既定値null)</param>
        /// <returns>取得した文字列</returns>
        public static string GetString(string section, string key, string inifilepath = null, string value = null)
        {
            if (string.IsNullOrWhiteSpace(inifilepath)) inifilepath = GetIniFile();
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, value, sb, sb.Capacity, inifilepath);
            return sb.ToString();
        }

        /// <summary>
        /// 定義ファイルに文字列を設定する
        /// </summary>
        /// <param name="section">設定するセクション名</param>
        /// <param name="key">設定するキー名</param>
        /// <param name="value">設定する値</param>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <returns>成否を表すtrue/false</returns>
        public static bool SetString(string section, string key, string value, string inifilepath = null)
        {
            if (string.IsNullOrWhiteSpace(inifilepath)) inifilepath = GetIniFile();
            return WritePrivateProfileString(section, key, value, inifilepath) != 0;
        }

        /// <summary>
        /// 定義ファイルから整数値を取得する
        /// </summary>
        /// <param name="section">取得するセクション名</param>
        /// <param name="key">取得するキー名</param>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <param name="value">取得できない場合に返すデフォルト値 (省略可、既定値0)</param>
        /// <returns>取得した値</returns>
        public static int GetInt(string section, string key, string inifilepath = null, int value = 0)
        {
            if (string.IsNullOrWhiteSpace(inifilepath)) inifilepath = GetIniFile();
            return GetPrivateProfileInt(section, key, value, inifilepath);
        }

        /// <summary>
        /// 定義ファイルに整数値を設定する
        /// </summary>
        /// <param name="section">設定するセクション名</param>
        /// <param name="key">設定するキー名</param>
        /// <param name="value">設定する値</param>
        /// <param name="inifilepath">INIファイルのパス (省略可)</param>
        /// <returns>成否を表すtrue/false</returns>
        public static bool SetInt(string section, string key, int value, string inifilepath = null)
        {
            return SetString(section, key, value.ToString(), inifilepath);
        }
    }

    /// <summary>
    /// セクション情報のコレクションクラス
    /// </summary>
    [ComVisible(false), Guid("E8B005A1-BCE5-42B9-8D90-B6C8DE63FAE9")]
    public class IniSections : Hashtable, IDisposable
    {
        /// <summary>
        /// セクション情報を扱うクラス
        /// </summary>
        [ComVisible(false), Guid("11FB47C0-5A04-43CA-B3D0-A32BB4172A52")]
        public class IniSection : IDisposable
        {
            private IniSections Collection = null;
            private string name = null;
            private IniKeys keys = null;

            /// <summary>
            /// キー情報のコレクションクラス
            /// </summary>
            [ComVisible(false), Guid("F8470DEB-FE2A-4FAA-8522-AB6DC5DE3441")]
            public class IniKeys : Hashtable, IDisposable
            {
                private IniSection section = null;

                /// <summary>
                /// キー情報を扱うクラス
                /// </summary>
                [ComVisible(false), Guid("27F63735-455F-4E26-B51F-0357C8E2977D")]
                public class IniKey : IDisposable
                {
                    private IniKeys Collection = null;
                    private string name = null;
                    private string value = null;

                    internal IniKey(IniKeys keys, string name)
                    {
                        Collection = keys;
                        this.name = name;
                        this.value = IniFile.GetString(ParentSection.Name, name, ParentSection.ParentCollection.IniFilePath);
                    }

                    /// <summary>
                    /// キー名を返す読み取り専用プロパティ
                    /// </summary>
                    public string Name
                    {
                        get
                        {
                            return name;
                        }
                    }

                    /// <summary>
                    /// キーの値を返す読み取り専用プロパティ
                    /// </summary>
                    public string Value
                    {
                        get
                        {
                            return value;
                        }
                    }

                    /// <summary>
                    /// 自身のインスタンスが含まれるコレクションへの参照を返す読み取り専用プロパティ
                    /// </summary>
                    public IniKeys ParentCollection
                    {
                        get
                        {
                            return Collection;
                        }
                    }

                    /// <summary>
                    /// 自身のインスタンスの親であるSectionクラスのインスタンスへの参照を返す読み取り専用プロパティ
                    /// </summary>
                    public IniSection ParentSection
                    {
                        get
                        {
                            if (Collection == null) return null;
                            return Collection.ParentSection;
                        }
                    }

                    /// <summary>
                    /// Disposeメソッドの実装
                    /// </summary>
                    public void Dispose()
                    {
                        Collection = null;
                    }
                }

                internal IniKeys(IniSection section)
                {
                    this.section = section;
                    foreach (string keyname in IniFile.GetKeys(section.Name, ParentSection.ParentCollection.IniFilePath))
                    {
                        Add(keyname);
                    }

                }

                private IniKey Add(string name)
                {
                    if (string.IsNullOrWhiteSpace(name)) return null;
                    if (this.Contains(name))
                    {
                        return this[name] as IniKey;
                    }
                    IniKey newKey = new IniKey(this, name);
                    this.Add(name, newKey);
                    return newKey;
                }

                /// <summary>
                /// 自身のインスタンスの親であるSectionクラスのインスタンスへの参照を返す読み取り専用プロパティ
                /// </summary>
                public IniSection ParentSection
                {
                    get
                    {
                        return section;
                    }
                }

                /// <summary>
                /// Disposeメソッドの実装
                /// </summary>
                public void Dispose()
                {
                    foreach (DictionaryEntry de in this)
                    {
                        IniKey key = de.Value as IniKey;
                        key.Dispose();
                    }
                }
            }

            internal IniSection(IniSections sections, string name)
            {
                Collection = sections;
                this.name = name;
                keys = new IniKeys(this);
            }

            /// <summary>
            /// セクション名を返す読み取り専用プロパティ
            /// </summary>
            public string Name
            {
                get
                {
                    return name;
                }
            }

            /// <summary>
            /// セクション内のキーコレクションを表すKeysクラスのインスタンスへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IniKeys Keys
            {
                get
                {
                    return keys;
                }
            }

            /// <summary>
            /// 自身のインスタンスが含まれるコレクションへの参照を返す読み取り専用プロパティ
            /// </summary>
            public IniSections ParentCollection
            {
                get
                {
                    return Collection;
                }
            }

            /// <summary>
            /// Disposeメソッドの実装
            /// </summary>
            public void Dispose()
            {
                keys.Dispose();
                Collection = null;
            }
        }

        private string inifilepath = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IniSections()
        {
            foreach (string sectionname in IniFile.GetSections(inifilepath))
            {
                Add(sectionname);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inifilepath">INIファイルのパス</param>
        public IniSections(string inifilepath)
        {
            if (!string.IsNullOrWhiteSpace(inifilepath))
            {
                this.inifilepath = inifilepath;
            }
        }

        private IniSection Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            if (this.Contains(name))
            {
                return this[name] as IniSection;
            }
            IniSection newSection = new IniSection(this, name);
            this.Add(name, newSection);
            return newSection;
        }

        /// <summary>
        /// INIファイルのパスを返す読み取り専用プロパティ
        /// </summary>
        public string IniFilePath
        {
            get
            {
                return inifilepath;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            foreach (DictionaryEntry de in this)
            {
                IniSection section = de.Value as IniSection;
                section.Dispose();
            }
        }
    }
}
#endif