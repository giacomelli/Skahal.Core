#region Usings
using System;
using Skahal.Infrastructure.Framework.Logging;
using System.Diagnostics;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{	
	public class SGN
	{
		#region Fields
		private static ISGNFactory s_factory;
		private static SGNReliableDataManager s_reliableDataManager; 
		#endregion
		
		#region Methods
		public static void Initialize (ISGNFactory factory)
		{
			if (!Initialized)
			{
				s_factory = factory;
				PlayerManager = factory.CreatePlayerManager ();
				
				MultiplayerManager = factory.CreateMultiplayerManager ();
				
				if(MultiplayerManager.Supported)
				{
					MultiplayerManager.Initialize ();
				}
				
				VoiceChatManager = factory.CreateVoiceChatManager ();
				LeaderboardManager = factory.CreateLeaderboardManager ();
				AchievementManager = factory.CreateAchievementManager ();
			
				UIManager = factory.CreateUIManager ();
			
				
				s_reliableDataManager = new SGNReliableDataManager();
				s_reliableDataManager.Initialize();
				
				PlayerManager.Login ();
				
				Initialized = true;
			}
			else
			{
				LogService.Error (@"SGN: the ISGNFactory can be set only one time. The current call to SetFactory with '{0}' is trying to overwrite the curent factory '{1}'.", factory, s_factory);
			}
		}	
		#endregion
		
		#region Properties
		public static bool Initialized { get; private set; }
		public static ISGNPlayerManager PlayerManager { get; private set; }
		public static ISGNMultiplayerManager MultiplayerManager { get; private set; }
		public static ISGNVoiceChatManager VoiceChatManager { get; private set; }
		public static ISGNUIManager UIManager { get; private set; }
		public static ISGNLeaderboardManager LeaderboardManager { get; private set; }
		public static ISGNAchievementManager AchievementManager { get; private set; }
		#endregion
	}
}