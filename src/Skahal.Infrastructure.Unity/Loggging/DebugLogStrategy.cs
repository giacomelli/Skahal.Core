#region Usings
using System;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Logging;
using UnityEngine;


#endregion

namespace Skahal.Infrastructure.Unity.Logging
{
	/// <summary>
	/// The ILogStrategy use during developement that use UnityEngine.Debug class to write logs.
	/// <remarks>Logs everythings.</remarks>
	/// </summary>
	public sealed class DebugLogStrategy : LogStrategyBase
	{	
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Unity.Logging.DebugLogStrategy"/> class.
		/// </summary>
		public DebugLogStrategy()
		{
			Application.RegisterLogCallback(CheckLogCallback);
		}
		
		private void CheckLogCallback (string condition, string stacktrace, LogType logType)
		{
			if (logType == LogType.Exception || logType == LogType.Assert) {
				WriteError(condition);
			}
		}
		#endregion

		#region ILogServiceStrategy implementation
		/// <summary>
		/// Writes the debug log level message.   
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public override void WriteDebug (string message, params object[] args)
		{
			var msg = String.Format (message, args);
			UnityEngine.Debug.Log ("[DEBUG]" + msg);
			OnDebugWritten(new LogWrittenEventArgs(msg));
		}
		
		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public override void WriteWarning (string message, params object[] args)
		{
			var msg = String.Format (message, args);
			UnityEngine.Debug.LogWarning ("[WARNING]" + String.Format (message, args));
			OnWarningWritten(new LogWrittenEventArgs (msg));
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public override void WriteError (string message, params object[] args)
		{
			var msg = String.Format (message, args);
			UnityEngine.Debug.LogError ("[ERROR]" + String.Format (message, args));
			OnErrorWritten(new LogWrittenEventArgs (msg));
		}
		#endregion
	}
}