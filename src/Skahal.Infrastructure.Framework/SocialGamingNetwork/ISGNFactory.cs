#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public interface ISGNFactory
	{	
		ISGNPlayerManager CreatePlayerManager();
		ISGNMultiplayerManager CreateMultiplayerManager();
		ISGNVoiceChatManager CreateVoiceChatManager();
		ISGNUIManager CreateUIManager();
		ISGNLeaderboardManager CreateLeaderboardManager();
		ISGNAchievementManager CreateAchievementManager();
	}
}