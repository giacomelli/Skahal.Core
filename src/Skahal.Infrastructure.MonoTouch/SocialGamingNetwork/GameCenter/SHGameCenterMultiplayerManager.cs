#region Usings
using System;
using System.Collections.Generic;
using MonoTouch.GameKit;
using Skahal.MonoTouch.Logging;
using MonoTouch.Foundation;
using Skahal.MonoTouch.UI;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// Game Center SGN multiplayer manager.
	/// </summary>
	public class SHGameCenterMultiplayerManager : ISGNMultiplayerManager
	{
		#region Fields
		private GKMatch m_match;
		#endregion
		
		#region Events
		public event EventHandler<OpponentConnectedEventArgs> OpponentConnected;
		public event EventHandler<OpponentDisconnectedEventArgs> OpponentDisconnected;
		public event EventHandler<InviteReceivedEventArgs> InviteReceived;
		public event EventHandler<ActivityOpponentsRefreshedEventArgs> ActivityOpponentsRefreshed;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets a value indicating whether multiplayer is supported.
		/// </summary>
		/// <value>
		/// <c>true</c> if supported; otherwise, <c>false</c>.
		/// </value>
		public bool Supported { get { return false; } }
		
		public bool IsHost 
		{
			get
			{
				if (SGN.PlayerManager.IsLogged)
				{
					var playerIds = m_match.PlayersIDs;
					
					if (playerIds.Length > 0)
					{
						return SGN.PlayerManager.Player.ID.GetHashCode() > playerIds[0].GetHashCode();
					}
				}
				
				return false;
			}
		}
	
		public IList<SGNPlayer> Opponents { get; private set; }
		#endregion
		
		#region Methods
		public void Initialize()
		{
			m_match = new GKMatch(NSObjectFlag.Empty);
			Opponents = new List<SGNPlayer>();
			
			/*
			GameCenterMultiplayerManager.playerConnected += HandleGameCenterMultiplayerManagerplayerConnected;
			GameCenterMultiplayerManager.playerDisconnected += HandleGameCenterMultiplayerManagerplayerDisconnected;
			
			GameCenterMultiplayerManager.inviteRequestWasReceived += HandleGameCenterMultiplayerManagerinviteRequestWasReceived;
			GameCenterMultiplayerManager.matchmakerFoundMatch += HandleGameCenterMultiplayerManagermatchmakerFoundMatch;
			
			GameCenterMultiplayerManager.findActivityFinished += delegate(int count)
			{
				if (ActivityOpponentsRefreshed != null)
				{
					ActivityOpponentsRefreshed(this, new ActivityOpponentsRefreshedEventArgs(count));
				}
			};
			*/
		}
		
		void HandleGameCenterMultiplayerManagerplayerConnected(string data)
		{
			ConnectPlayer(data);
		}
		
		void HandleGameCenterMultiplayerManagerplayerDisconnected(string data)
		{
			SHLog.Debug("SGN[user disconnected]: " + data);
			
			if (OpponentDisconnected != null)
			{
				var opponent = new SGNPlayer(data);
				Opponents.Remove(opponent);
				
				OpponentDisconnected(this, new OpponentDisconnectedEventArgs(opponent));
			}
		}
		
		void HandleGameCenterMultiplayerManagerinviteRequestWasReceived()
		{
			SHLog.Debug("SGN[InviteReceived]");
			if (InviteReceived != null)
			{
				InviteReceived(this, new InviteReceivedEventArgs());
			}
		}
		
		void HandleGameCenterMultiplayerManagermatchmakerFoundMatch(int count)
		{
			var connectedPlayerIds = m_match.PlayersIDs;
			
			foreach (var playerId in connectedPlayerIds)
			{
				ConnectPlayer(playerId);
			}
		}
		
		void ConnectPlayer(string playerId)
		{
			if (!String.IsNullOrEmpty(playerId.Trim()))
			{
				SHLog.Debug("SGN[user connected]: " + playerId);
				var opponent = new SGNPlayer(playerId);
				Opponents.Add(opponent);
			
				if (OpponentConnected != null)
				{
					OpponentConnected(this, new OpponentConnectedEventArgs(opponent));
				}
			}
		}
	
		public void SendMessage(string messageName, string messageValue)
		{
			var packet = NSData.FromString(String.Format("{0}___{1}", messageName, messageValue));		                               
			m_match.SendDataToAllPlayers(packet, GKMatchSendDataMode.Reliable, IntPtr.Zero);
		}
	
		public void CloseSession()
		{
			SHLog.Debug("SHGameCenterMultiplayerManager[CloseSession]");
			m_match.Disconnect();
		}
	
		public void ShowAvailableOpponents()
		{
			var matchRequest = new GKMatchRequest();
			matchRequest.MinPlayers = 2;
			matchRequest.MaxPlayers = 2;
			
			var vc = new GKMatchmakerViewController(matchRequest);
			vc.PresentModal();
		}
		
		public void RefreshActivityOpponents()
		{
			//GameCenterMultiplayerBinding.findAllActivity();
		}
		#endregion
	}
}