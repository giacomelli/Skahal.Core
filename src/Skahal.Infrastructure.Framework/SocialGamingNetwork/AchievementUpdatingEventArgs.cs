#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class AchievementUpdatingEventArgs : EventArgs
	{
		#region Constructors
		public AchievementUpdatingEventArgs(SGNAchievement achievement)
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