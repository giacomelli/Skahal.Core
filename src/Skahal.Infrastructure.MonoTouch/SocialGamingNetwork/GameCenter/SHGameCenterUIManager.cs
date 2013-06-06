#region Usings
using System;
using MonoTouch.GameKit;
using Skahal.MonoTouch.UI;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// The Game Center user interface manager.
	/// </summary>
	public class SHGameCenterUIManager : ISGNUIManager
	{
		#region Constructors
		public SHGameCenterUIManager()
		{
			AllowNotifications = true;
			
//			SGN.AchievementManager.AchievementUnlocked += delegate(object sender, AchievementUnlockedEventArgs e) {
//				if(AllowNotifications)
//				{
//					GKNotificationBanner.Show(e.Achievement.Name, e.Achievement.Name, () => {});
//				}
//			};
			
			SGN.PlayerManager.LoggedIn += delegate(object sender, PlayerLoggedInEventArgs e) {
				if(AllowNotifications)
				{
					var msg = String.Format("Welcome back, {0}.", e.Player.Name);
					GKNotificationBanner.Show(null, msg, () => {});
				}
			};
		}
		#endregion
		
		#region ISGNUIManager implementation
		public void ShowLeaderboards()
		{
			var gkvc = new GKLeaderboardViewController();
			gkvc.Delegate = new SHGameCenterLeaderboardViewControllerDelegate();
			gkvc.PresentModal();
		}
	
		public void ShowAchievements()
		{
			var gkvc = new GKAchievementViewController();
			gkvc.Delegate = new SHGameCenterAchievementViewControllerDelegate();
			gkvc.PresentModal();
		}
	
		public bool AllowNotifications 
		{
			get; set;
		}
		#endregion	
	}
}