#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class LeaderboardUpdatingEventArgs : EventArgs
	{
		#region Constructors
		public LeaderboardUpdatingEventArgs(SGNLeaderboard leaderboard)
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