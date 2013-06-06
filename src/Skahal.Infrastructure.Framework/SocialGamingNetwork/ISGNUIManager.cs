#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public interface ISGNUIManager
	{
		#region Properties
		bool AllowNotifications
		{
			get;
			set;
		}
		#endregion
		
		#region Methods
		void ShowLeaderboards();
		void ShowAchievements();
		#endregion
	}
}