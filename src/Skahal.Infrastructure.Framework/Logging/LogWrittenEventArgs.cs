#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Logging
{
	/// <summary>
	/// SH log written event arguments.
	/// </summary>
	public class LogWrittenEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Logging.LogWrittenEventArgs"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		public LogWrittenEventArgs (string message)
		{
			Message = message;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; private set;}
		#endregion
	}
}