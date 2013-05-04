#region Usings
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
using Skahal.Domain.People;


#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// The Global Server client.
	/// </summary>
	public class GlobalServerClient {	
		#region Events
		/// <summary>
		/// Occurs when message received.
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageReceived;

		/// <summary>
		/// Occurs when game created.
		/// </summary>
		public event EventHandler<GlobalServerGameCreatedEventArgs> GameCreated;

		/// <summary>
		/// Occurs when peer disconnected.
		/// </summary>
		public event EventHandler PeerDisconnected;

		/// <summary>
		/// Occurs when server info received.
		/// </summary>
		public event EventHandler<GlobalServerInfoReceivedEventArgs> ServerInfoReceived;
		#endregion
		
		#region Fields
		private Player m_player;
		private Queue<Action> m_requestsQueue = new Queue<Action> ();
		private bool m_isWaitingForRequestResponse;
		private bool m_isWaitingForReceiveMessage;
		private bool m_isHost;
		private bool m_useP2P;
		private bool m_keepReceiving;
		private IGlobalServerMessagingStrategy m_messagingStrategy;
		private float m_minReceiveMessagesWaitingSeconds = 1f;
		private float m_maxReceiveMessagesWaitingSeconds = 1f;
		private float m_decreaseReceiveMessagesWaitingSeconds = 0f;
		private int m_lastDequeuedMessageId;
		private string m_serverAddress;
		private string m_multiplayerVersion;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer.GlobalServerClient"/> class.
		/// </summary>
		/// <param name="player">The player using the client.</param>
		/// <param name="serverAddress">Server address.</param>
		/// <param name="multiplayerVersion">Multiplayer version.</param>
		public GlobalServerClient (Player player, string serverAddress, string multiplayerVersion)
		{
			m_player = player;

			if(string.IsNullOrEmpty(m_player.RemoteId))
			{
				m_player.RemoteId = Guid.NewGuid ().ToString ();
			}

			m_serverAddress = serverAddress;
			m_multiplayerVersion = multiplayerVersion;
			Instance = this;
			m_keepReceiving = true;
			
			//m_useP2P = Network.HavePublicAddress ();//nat != ConnectionTesterStatus.Error && nat != ConnectionTesterStatus.Undetermined
			//var nat = Network.TestConnectionNAT ();
			
			//LogService.Warning ("ExternalAddress: {0}:{1}.||||", Network.player.externalIP, Network.player.externalPort);
			//LogService.Warning ("GlobalServerCommunicator: using P2P: '{0}' : {1} : {2}:{3}.", m_useP2P, nat, Network.player.externalIP, Network.player.externalPort);
			m_useP2P = false;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static GlobalServerClient Instance { get; private set; }
		#endregion
		
		#region Public methods
		/// <summary>
		/// Connect this instance.
		/// </summary>
		public void Connect ()
		{
			ConfigureMessagingStrategy(m_useP2P);
				
			Request ("Multiplayer/RegisterPlayer", GetPlayerArgs());
		}

		/// <summary>
		/// Disconnect this instance.
		/// </summary>
		public void Disconnect ()
		{
			Request (
					"Multiplayer/UnregisterPlayer",
					"playerId", m_player.RemoteId);
			
			m_messagingStrategy.Disconnect ();
			m_keepReceiving = false;
		}

		/// <summary>
		/// Refreshs the server info.
		/// </summary>
		public void RefreshServerInfo ()
		{
			LogService.Warning ("ExternalAddress: {0}", Network.player.externalIP);
			Request (
			(string v) =>
			{
				var r = v.hashtableFromJson ();
				
				if (r != null && r.Count > 0)
				{
					var availablePlayersCount = Convert.ToInt32 (r ["AvailablePlayersCount"]);
					
					// If return more than zero players, so discount the current player.
					if (availablePlayersCount > 0)
					{
						availablePlayersCount--;
					}
					
					var gamesCount = Convert.ToInt32 (r ["GamesCount"]);
					m_minReceiveMessagesWaitingSeconds = Convert.ToSingle (r ["MinDequeueMessagesWaitingSeconds"]);
					m_maxReceiveMessagesWaitingSeconds = Convert.ToSingle (r ["MaxDequeueMessagesWaitingSeconds"]);
					m_decreaseReceiveMessagesWaitingSeconds = Convert.ToSingle (r ["DecreaseDequeueMessagesWaitingSeconds"]);
					var isOnline = "Online".Equals(r["State"]);
					
					var args = new GlobalServerInfoReceivedEventArgs (availablePlayersCount, gamesCount, isOnline);
					ServerInfoReceived.Raise (this, args);
				}	
			},
			"Server/GetServer", GetPlayerArgs ());
		}

		/// <summary>
		/// Creates the game.
		/// </summary>
		public void CreateGame ()
		{
			Request (
			(v) =>
			{
				var r = v.hashtableFromJson();
				
				if (r != null && r.Count > 0)
				{
					m_isHost = true;
				}	
						
				SH.StartCoroutine (ReceiveMessage ());
			},
			"Multiplayer/CreateGame",
			"playerId", m_player.RemoteId);
		}

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="messageName">Message name.</param>
		/// <param name="messageValue">Message value.</param>
		public void SendMessage (string messageName, string messageValue)
		{
			m_messagingStrategy.SendMessage(messageName, messageValue);
		}	

		/// <summary>
		/// Request the specified serviceName and args.
		/// </summary>
		/// <param name="serviceName">Service name.</param>
		/// <param name="args">Arguments.</param>
		public void Request (string serviceName, params string[] args)
		{
			Request ((r) => {}, serviceName, args);
		}

		/// <summary>
		/// Configures the messaging strategy.
		/// </summary>
		/// <param name="useP2P">If set to <c>true</c> use P2P.</param>
		public void ConfigureMessagingStrategy (bool useP2P)
		{
			m_messagingStrategy = useP2P 
				? (IGlobalServerMessagingStrategy)new GlobalServerP2PMessagingStrategy () 
					: (IGlobalServerMessagingStrategy)new GlobalServerAuthoritativeServerMessagingStrategy (this);
		}

		#endregion
		
		#region Private methods
		private void Request (Action<string>responseReceivedAction, string serviceName, params string[] args)
		{
			lock (this)
			{
				Action action = () =>
				{
					SH.StartCoroutine (BeginRequest (responseReceivedAction, serviceName, args));	
				};
					
				LogService.Warning ("GlobalServerCommunicator: enqueue action for service '{0}'...", serviceName);
				m_requestsQueue.Enqueue (action);
				NextRequest ();
			}
		}
			
		private void NextRequest ()
		{
			if (!m_isWaitingForRequestResponse && m_requestsQueue.Count > 0)
			{
				m_requestsQueue.Peek()();
			}
		}
			
		private IEnumerator BeginRequest (Action<string>responseReceivedAction, string serviceName, params string[] args)
		{
			lock (this)
			{
				m_isWaitingForRequestResponse = true;
						
				try
				{
					var url = new StringBuilder ();
					url.AppendFormat ("{0}/Services/{1}.ashx?", m_serverAddress, serviceName);
					
					if (args != null)
					{
						for (int i = 0; i < args.Length; i += 2)
						{
							url.AppendFormat ("{0}={1}&", args [i], WWW.EscapeURL (args [i + 1]));
						}
					}
					
					var plainUrl = url.ToString ();
					LogService.Warning ("GlobalServerCommunicator: requesting url '{0}'...", plainUrl);
					var www = new WWW (plainUrl);
					yield return www;
					
					if (String.IsNullOrEmpty (www.error))
					{
						var response = www.text;
					
						if (!String.IsNullOrEmpty (response))
						{
							LogService.Warning ("GlobalServerCommunicator: response received '{0}'.", response);
						}
						
						// If no error while requesting, so dequeue the action.
						m_requestsQueue.Dequeue ();
						responseReceivedAction (response);
					}
					else
					{
						yield return new WaitForSeconds(5f);
					}
				}
				finally
				{
					m_isWaitingForRequestResponse = false;
					NextRequest ();	
				}
			}
		}
		
		private IEnumerator ReceiveMessage ()
		{
			float waitingSeconds = m_maxReceiveMessagesWaitingSeconds;
			
			while (m_keepReceiving)
			{
				yield return new WaitForSeconds(waitingSeconds);
				
				if (!m_isWaitingForReceiveMessage)
				{
					m_isWaitingForReceiveMessage = true;
			
					Request (
					(v) =>
					{
						try
						{
							var r = v.arrayListFromJson ();
						
							// Receive no messages, so trying again in lower interval than current.
							if (r == null)
							{
								waitingSeconds -= m_decreaseReceiveMessagesWaitingSeconds;
								
								if (waitingSeconds < m_minReceiveMessagesWaitingSeconds)
								{
									waitingSeconds = m_maxReceiveMessagesWaitingSeconds;
								}
							}
							else
							{
								LogService.Debug ("GlobalServerCommunicator: DequeueMessages: {0}", r.Count > 0 ? r [0] : null);
								
								// Receive messages, so make the interval greater than current.
								waitingSeconds = m_maxReceiveMessagesWaitingSeconds;
						
								foreach (Hashtable msg in r)
								{
									ProccessMessage (msg);	
								}
							}
						}
						finally
						{
							m_isWaitingForReceiveMessage = false;
						}
					},
				"Multiplayer/DequeueMessages",
				"playerId", m_player.RemoteId,
				"LastDequeuedMessageId", m_lastDequeuedMessageId.ToString());
				}
			}	
		}
		
		private void ProccessMessage (Hashtable r)
		{
			if (r.Count == 0)
			{
				LogService.Debug ("GlobalServerCommunicator: empty message received.");
			}
			else
			{
				m_lastDequeuedMessageId = Convert.ToInt32 (r ["Id"]);
				var messageName = r ["Name"].ToString ();
				var messageValue = r ["Value"];
				LogService.Debug ("GlobalServerCommunicator: Name:{0} - Value:{1}.", messageName, messageValue);
							
				switch (messageName)
				{
				case "GameCreated":
					var value = (Hashtable)messageValue;
					var enemy = new GlobalServerPlayer ();
					enemy.Name = value ["Name"].ToString ();
					enemy.RemoteId = value ["Id"].ToString ();
					enemy.IP = value ["IP"].ToString ();
					enemy.Device = value ["Device"].ToString ();
								
					ConfigureMessagingStrategy (!String.IsNullOrEmpty (enemy.IP));
								
								
					m_messagingStrategy.MessageReceived += (sender, e) => {
						MessageReceived.Raise (this, e);
					};
		
					m_messagingStrategy.PeerDisconnected += (sender, e) => {
						PeerDisconnected.Raise (this);
					};
								
					m_keepReceiving = !m_messagingStrategy.Connect (m_isHost, enemy);
					GameCreated.Raise (this, new GlobalServerGameCreatedEventArgs (m_isHost, enemy));
					break;
					
				case "GameEnded":
					m_keepReceiving = false;
					m_messagingStrategy.ReceiveMessage (messageName, messageValue.ToString());
					break;
					
				default:
					m_messagingStrategy.ReceiveMessage (messageName, messageValue.ToString());
					break;
				}
			}	
		}

		private string[] GetPlayerArgs ()
		{
			return new string[] 
			{
				"id", m_player.RemoteId,
				"name", SanitizeInfoFromPlayer (m_player.Name),
				"Device", SHDevice.Family.ToString (),
				"IP", m_messagingStrategy.PlayerAddress,
				"GameVersion", m_multiplayerVersion
			};
		}
		
		private string SanitizeInfoFromPlayer(string info)
		{
			return info.Replace ("<", "").Replace(">", "");
		}
		#endregion
	}
}