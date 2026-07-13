using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using log4net;
using log4net.Appender;
using log4net.Config;

namespace QC4Common.Util
{
	public class LogFileCleanupTask
	{
		#region - Constructor -
		public LogFileCleanupTask()
		{
		}
		#endregion

		#region - Methods -
		/// <summary>
		/// Cleans up. Auto configures the cleanup based on the log4net configuration
		/// </summary>
		/// <param name="date">Anything prior will not be kept.</param>
		public void CleanUp(DateTime date)
		{
			string directory = string.Empty;
			string filePrefix = string.Empty;

			var repo = LogManager.GetAllRepositories().FirstOrDefault(); ;
			if (repo == null)
				throw new NotSupportedException("Log4Net has not been configured yet.");

			var app = repo.GetAppenders().Where(x => x.GetType() == typeof(RollingFileAppender)).FirstOrDefault();
			if (app != null)
			{
				var appender = app as RollingFileAppender;

				directory = Path.GetDirectoryName(appender.File);
				filePrefix = Path.GetFileName(appender.File);

				CleanUp(directory, filePrefix, date);
			}
		}

		/// <summary>
		/// Cleans up.
		/// </summary>
		/// <param name="logDirectory">The log directory.</param>
		/// <param name="logPrefix">The log prefix. Example: logfile dont include the file extension.</param>
		/// <param name="date">Anything prior will not be kept.</param>
		public void CleanUp(string logDirectory, string logPrefix, DateTime date)
		{
			if (string.IsNullOrEmpty(logDirectory))
				throw new ArgumentException("logDirectory is missing");

			if (string.IsNullOrEmpty(logPrefix))
				throw new ArgumentException("logPrefix is missing");

			var dirInfo = new DirectoryInfo(logDirectory);
			if (!dirInfo.Exists)
				return;

            var fileInfos = dirInfo.GetFiles("{0}*.*".Sub(logPrefix));
			if (fileInfos.Length == 0)
				return;

			foreach (var info in fileInfos)
			{
				if (info.LastWriteTime < date)
				{
					info.Delete();
				}
			}

		}
		#endregion
	}

	/// <summary>
	/// Extension helper methods for strings
	/// </summary>
	[DebuggerStepThrough, DebuggerNonUserCode]
	public static class StringExtensions
	{
		/// <summary>
		/// Formats a string using the <paramref name="format"/> and <paramref name="args"/>.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		/// <returns>A string with the format placeholders replaced by the args.</returns>
		public static string Sub(this string format, params object[] args)
		{
			return string.Format(format, args);
		}
	}
}
