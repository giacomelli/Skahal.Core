#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class LeaderboardUpdatingFailedEventArgs : EventArgs
	{
		#region Constructors
		public LeaderboardUpdatingFailedEventArgs(SGNLeaderboard leaderboard)
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