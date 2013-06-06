#region Usings
using System;
using Skahal.MonoTouch.Logging;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	internal static class SHGameCenterHelper
	{
		#region Methods
		public static void Log(string className, string methodName, string message, params object[] args)
		{
			string msg;
			
			if (args == null)
			{
				msg = String.Format("[{0}.{1}] {2}", className, methodName, message);
			}
			else
			{
				msg = String.Format("[{0}.{1}] {2}", className, methodName, String.Format(message, args));
			}
			
			SHLog.Debug(msg);
		}
		#endregion
	}
}