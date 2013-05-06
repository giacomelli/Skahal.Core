using System;
using System.IO;

namespace Skahal.Infrastructure.Unity.Logging
{
	/// <summary>
	/// Log helper.
	/// </summary>
	public static class LogHelper
	{
		/// <summary>
		/// Reads all player log.
		/// </summary>
		/// <returns>The all player log.</returns>
		public static string ReadAllPlayerLog()
		{
			return File.ReadAllText("~/Library/Logs/Unity/Player.log");
		}
	}
}