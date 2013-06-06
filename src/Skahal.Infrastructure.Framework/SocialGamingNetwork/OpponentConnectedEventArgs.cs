#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class OpponentConnectedEventArgs : System.EventArgs
	{
		#region Constructors
		public OpponentConnectedEventArgs(SGNPlayer opponent)
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