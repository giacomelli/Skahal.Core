#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class AchievementsRefreshedEventArgs : EventArgs
	{
		#region Constructors
		public AchievementsRefreshedEventArgs(SGNAchievement[] achievements)
		{
			Achievements = achievements;	
		}
		#endregion
		
		#region Properties
		public SGNAchievement[] Achievements
		{
			get;
			private set;
		}
		#endregion
	}
}