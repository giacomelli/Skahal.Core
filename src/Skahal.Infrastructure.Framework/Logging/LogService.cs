#region Usings
using System;
using Skahal.Infrastructure.Framework.Commons;
#endregion

namespace Skahal.Infrastructure.Framework.Logging
{
	/// <summary>
	/// A central point to organize logs.
	/// </summary>
	public static class LogService
	{
		#region Events
		/// <summary>
		/// Occurs when a debug log is written.
		/// </summary>
		public static event EventHandler<LogWrittenEventArgs> DebugWritten;

		/// <summary>
		/// Occurs when a warning log is written.
		/// </summary>
		public static event EventHandler<LogWrittenEventArgs> WarningWritten;

		/// <summary>
		/// Occurs when a error log is written.
		/// </summary>
		public static event EventHandler<LogWrittenEventArgs> ErrorWritten;
		#endregion
		
		#region Fields
		/// <summary>
		/// The log strategy.
		/// </summary>
		private static ILogStrategy s_logStrategy;
		#endregion

		#region Methods
		/// <summary>
		/// Initialize the specified logStrategy.
		/// </summary>
		/// <param name="logStrategy">Log strategy.</param>
		public static void Initialize (ILogStrategy logStrategy)
		{
			s_logStrategy = logStrategy;
			
			s_logStrategy.DebugWritten += delegate(object sender, LogWrittenEventArgs e) {
				DebugWritten.Raise (typeof(LogService), e);
			};
			
			s_logStrategy.WarningWritten += delegate(object sender, LogWrittenEventArgs e) {
				WarningWritten.Raise (typeof(LogService), e);
			};
			
			s_logStrategy.ErrorWritten += delegate(object sender, LogWrittenEventArgs e) {
				ErrorWritten.Raise (typeof(LogService), e);
			};
		}

		/// <summary>
		/// Write a debug log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Debug (string message, params object[] args)
		{
			s_logStrategy.WriteDebug(message, args);
		}
		
		/// <summary>
		/// Write a warning log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Warning (string message, params object[] args)
		{
			s_logStrategy.WriteWarning(message, args);
		}
		
		/// <summary>
		/// Write an error log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Error (string message, params object[] args)
		{
			s_logStrategy.WriteError(message, args);
		}
		
		/// <summary>
		/// Write an error log level.
		/// </summary>
		/// <param name='ex'>
		/// The exception about the error log level.
		/// </param>
		public static void Error (Exception ex)
		{
			s_logStrategy.WriteError(ex);
		}
		#endregion
	}
}