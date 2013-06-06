#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public interface ISGNVoiceChatManager
	{
		bool Supported { get; }
		bool Enabled { get; set; }
	}
}