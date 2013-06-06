#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{	
	public class AchievementUpdatedEventArgs : EventArgs
	{
		#region Constructors
		public AchievementUpdatedEventArgs(SGNAchievement achievement)
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