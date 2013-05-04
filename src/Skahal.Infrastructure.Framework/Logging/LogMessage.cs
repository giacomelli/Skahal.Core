using System;

namespace Skahal.Infrastructure.Framework.Logging
{
	/// <summary>
	/// Represents a log message.
	/// </summary>
	internal class LogMessage
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Logging.LogMessage"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="arguments">Arguments.</param>
		public LogMessage(string message, params object[] arguments)
		{
			Message = message;
			Arguments = arguments;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the arguments.
		/// </summary>
		/// <value>The arguments.</value>
		public object[] Arguments { get; set; }
		#endregion
	}
}