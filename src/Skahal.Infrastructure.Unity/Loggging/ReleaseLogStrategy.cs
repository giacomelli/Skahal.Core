#region Usings
using System;
using Skahal.Infrastructure.Framework.Logging;


#endregion

namespace Skahal.Infrastructure.Unity.Logging
{
	/// <summary>
	/// The ILogStrategy use when the games goes live in a release build.
	/// <remarks>Logs only erros.</remarks>
	/// </summary>
	public sealed class ReleaseLogStrategy : LogStrategyBase
	{			
		#region ISHLogStrategy implementation
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
			// Should not log warning messages in a release build.
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
			// Should not log warning messages in a release build.
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
			UnityEngine.Debug.LogError ("[ERROR]" + String.Format (message, args));
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		public override void WriteError (Exception ex)
		{
			WriteError ("[ERROR] {0}", ex.Message);
		}
		#endregion
	}
}