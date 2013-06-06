#region Usings
using System;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// The Social Gaming Network factory for Game Center.
	/// </summary>
	public class SHGameCenterFactory : ISGNFactory
	{
		#region ISGNFactory implementation
		/// <summary>
		/// Creates the player manager.
		/// </summary>
		/// <returns>
		/// The player manager.
		/// </returns>
		public ISGNPlayerManager CreatePlayerManager ()
		{
			return new SHGameCenterPlayerManager ();
		}
		
		/// <summary>
		/// Creates the multiplayer manager.
		/// </summary>
		/// <returns>
		/// The multiplayer manager.
		/// </returns>
		public ISGNMultiplayerManager CreateMultiplayerManager ()
		{
			return new SHGameCenterMultiplayerManager ();
		}
		
		/// <summary>
		/// Creates the voice chat manager.
		/// </summary>
		/// <returns>
		/// The voice chat manager.
		/// </returns>
		public ISGNVoiceChatManager CreateVoiceChatManager ()
		{
			return new SHGameCenterVoiceChatManager ();
		}
		
		/// <summary>
		/// Creates the user interface manager.
		/// </summary>
		/// <returns>
		/// The user interface manager.
		/// </returns>
		public ISGNUIManager CreateUIManager ()
		{
			return new SHGameCenterUIManager ();
		}
		
		/// <summary>
		/// Creates the leaderboard manager.
		/// </summary>
		/// <returns>
		/// The leaderboard manager.
		/// </returns>
		public ISGNLeaderboardManager CreateLeaderboardManager ()
		{
			return new SHGameCenterLeaderboardManager ();
		}
		
		/// <summary>
		/// Creates the achievement manager.
		/// </summary>
		/// <returns>
		/// The achievement manager.
		/// </returns>
		public ISGNAchievementManager CreateAchievementManager()
		{
			return new SHGameCenterAchievementManager();
		}
		#endregion
	}
}