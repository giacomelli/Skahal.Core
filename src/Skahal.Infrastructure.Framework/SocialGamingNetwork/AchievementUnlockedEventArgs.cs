#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class AchievementUnlockedEventArgs : EventArgs
	{
		#region Constructors
		public AchievementUnlockedEventArgs(SGNAchievement achievement)
		{
			Achievement = achievement;
		}
		#endregion
		
		#region Properties
		public SGNAchievement Achievement
		{
			get;
			private set;
		}
		#endregion
	}
}