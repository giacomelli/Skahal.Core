#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class OpponentDisconnectedEventArgs : System.EventArgs
	{
		#region Constructors
		public OpponentDisconnectedEventArgs(SGNPlayer opponent)
		{
			Opponent = opponent;
		}
		#endregion
		
		#region Properties
		public SGNPlayer Opponent
		{
			get;
			private set;
		}
		#endregion
	}
}