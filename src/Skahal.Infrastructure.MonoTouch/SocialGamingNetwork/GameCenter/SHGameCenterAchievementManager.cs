#region Usings
using System;
using MonoTouch.GameKit;
using Skahal.MonoTouch.Common;
using Skahal.MonoTouch.Logging;
using System.Collections.Generic;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	public class SHGameCenterAchievementManager : ISGNAchievementManager
	{
		#region Events
		public event EventHandler<AchievementUnlockedEventArgs> AchievementUnlocked;
		public event EventHandler<AchievementsRefreshedEventArgs> AchievementsRefreshed;
		public event EventHandler<AchievementUpdatingEventArgs> AchievementUpdating;
		public event EventHandler<AchievementUpdatingFailedEventArgs> AchievementUpdatingFailed;
		public event EventHandler<AchievementUpdatedEventArgs> AchievementUpdated;
		#endregion
			
		#region Properties
		public bool Supported { get { return true; }}
		#endregion
		
		#region Methods
		public void UpdateAchievement(SGNAchievement achievement)
		{
			AchievementUpdating.Raise(this, new AchievementUpdatingEventArgs(achievement));
			
			var gka = new GKAchievement(achievement.ID);
			gka.PercentComplete = achievement.Percent;
			gka.ShowsCompletionBanner = SGN.UIManager.AllowNotifications;
			
			gka.ReportAchievement((error) => 
            {
				if(error == null)
				{
					AchievementUpdated.Raise(this, new AchievementUpdatedEventArgs(achievement));
				}
				else
				{
					AchievementUpdatingFailed.Raise (this, new AchievementUpdatingFailedEventArgs(achievement));
				}
			});
			
			if (achievement.Percent == 100)
			{
				AchievementUnlocked.Raise(this, new AchievementUnlockedEventArgs(achievement));
			}
		}
		
		public void ResetAchievements()
		{
			GKAchievement.ResetAchivements((error) => 
			{
				if(error == null)
				{
					SHLog.Debug("SGN: achievements resets on server.");
				}
				else
				{
					SHLog.Error(error.LocalizedDescription);
				}
			});
		}
		
		public void RefreshAchievements()
		{
			GKAchievement.LoadAchievements((achievements, error) => 
			{
				if(error == null)
				{
					if (AchievementsRefreshed != null)
					{
						var sgnAchievements = new List<SGNAchievement>();
						
						foreach (var gcAchievement in achievements)
						{
							var sgnAchievement = new SGNAchievement(gcAchievement.Identifier);
							sgnAchievement.Percent = Convert.ToSingle(gcAchievement.PercentComplete);
							sgnAchievements.Add(sgnAchievement);
						}
						
						AchievementsRefreshed(this, new AchievementsRefreshedEventArgs(sgnAchievements.ToArray()));
					}	
				}
				else
				{
					SHLog.Error(error.LocalizedDescription);
				}
			});
		}
		#endregion
	}
}