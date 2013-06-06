#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class PlayerLoggedInEventArgs : System.EventArgs
	{
		#region Constructors
		public PlayerLoggedInEventArgs(SGNPlayer player)
		{
			Player = player;	
		}
		#endregion
		
		#region Properties
		public SGNPlayer Player
		{
			get;
			private set;
		}
		#endregion
	}
}