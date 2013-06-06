#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class LeaderboardUpdatedEventArgs : EventArgs
	{
		#region Constructors
		public LeaderboardUpdatedEventArgs(SGNLeaderboard leaderboard)
		{
			Leaderboard = leaderboard;
		}
		#endregion
		
		#region Properties
		public SGNLeaderboard Leaderboard
		{
			get; 
			private set;
		}
		#endregion
	}
}