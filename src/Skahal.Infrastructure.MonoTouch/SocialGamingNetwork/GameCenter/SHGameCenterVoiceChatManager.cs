#region Usings
using System;
using MonoTouch.GameKit;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	public class SHGameCenterVoiceChatManager : ISGNVoiceChatManager
	{
		#region Fields
		private bool m_enabled;
		#endregion
		
		#region ISGNVoiceChatManager implementation
		public bool Supported 
		{
			get { return false; }
		}
	
		public bool Enabled
		{
			get { return m_enabled; }
	
			set {
				m_enabled = value;
			}
		}
		#endregion
	}
}