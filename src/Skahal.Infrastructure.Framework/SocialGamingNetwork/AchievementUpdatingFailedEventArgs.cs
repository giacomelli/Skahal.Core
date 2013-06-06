#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class AchievementUpdatingFailedEventArgs : EventArgs
	{
		#region Constructors
		public AchievementUpdatingFailedEventArgs(SGNAchievement achievement)
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