#region Usings
using System;
using MonoTouch.GameKit;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// The Game Center player manager for SGN.
	/// </summary>
	public class SHGameCenterPlayerManager : ISGNPlayerManager
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.MonoTouch.SocialGamingNetwork.GameCenter.SHGameCenterPlayerManager"/> class.
		/// </summary>
		public SHGameCenterPlayerManager()
		{
			Player = new SGNPlayer(string.Empty);	
			CurrentLocalPlayer = new GKLocalPlayer();
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the current local player.
		/// </summary>
		/// <value>
		/// The current local player.
		/// </value>
		public GKLocalPlayer CurrentLocalPlayer { get; private set; }
		#endregion
		
		#region ISGNPlayerManager implementation
		public event EventHandler<PlayerLoggedInEventArgs> LoggedIn;
	
		public void Login()
		{
			CurrentLocalPlayer.Authenticate((error) => 
			{
				if(CurrentLocalPlayer.Authenticated)
				{
					Player = new SGNPlayer(CurrentLocalPlayer.PlayerID);
					Player.Name = CurrentLocalPlayer.Alias;
				
					if (LoggedIn != null)
					{
						LoggedIn(this, new PlayerLoggedInEventArgs(Player));
					}
				}
			});
		}
		
		
		public SGNPlayer Player 
		{
			get;
			private set;
		}
	
		public bool IsLogged 
		{
			get 
			{
				return CurrentLocalPlayer.Authenticated;
			}
		}
		#endregion
	}
}