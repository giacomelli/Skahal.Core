#region Usings
using Skahal.Infrastructure.Framework.Net.Messaging;
using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine;
using Skahal.Infrastructure.Unity.Logging;
using System.Linq;
using Skahal.Infrastructure.Framework.Logging;


#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Photon
{
	/// <summary>
	/// A Photon IMessenger implementation.
	/// </summary>
	public class PhotonMessenger : MessengerBase
	{
		#region Fields
	//	private PhotonClient m_client;
	//	private bool m_isServer;
		#endregion
		
		#region IRemoteMessenger implementation
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		/// <param name="isServer">If set to <c>true</c> is server.</param>
		public override void Connect (bool isServer)
		{
			var m_proxy = new GameObject ("PhotonMessengerProxy").AddComponent<PhotonMessengerProxy> ();
		
			m_proxy.Connected += delegate {
				OnConnected();
			};
			//m_proxy.Connect();

//			var settings = new ServerSettings();
//			settings.AppID = "d337efed-5773-4bde-a10a-978cfa24697a";
//			settings.ServerAddress = "app-eu.exitgamescloud.com";
//			settings.ServerPort = 5055;
//			PhotonNetwork.PhotonServerSettings = new ServerSettings();

			PhotonNetwork.autoJoinLobby = true;
			PhotonNetwork.ConnectUsingSettings("0.1");
		
//		//	m_isServer = isServer;
//			m_client = new PhotonClient ();
//			//m_client.AutoJoinLobby = true;
//			m_client.AppId = "d337efed-5773-4bde-a10a-978cfa24697a";
//			m_client.MasterServerAddress = "app.exitgamescloud.com:5055";
//	
//			
//			m_client.StatusChanged += delegate(object sender, PhotonStatusChangedEventArgs e) {
//		
//				switch (e.StatusCode) {
//				case StatusCode.EncryptionEstablished:
//					
//					break;
//				
//				case StatusCode.Disconnect:
//				case StatusCode.DisconnectByServer:
//				case StatusCode.DisconnectByServerLogic:
//				case StatusCode.DisconnectByServerUserLimit:
//					OnDisconnected (new DisconnectedEventArgs (DisconnectionReason.ConnectionLost));
//					break;
//				}
//			};
//			
//			m_client.MessageReceived += delegate(object sender, MessageReceivedEventArgs e) {
//				OnMessageReceived (e);
//			};
//			
//			m_client.ConnectToMaster (m_client.AppId, "1.0", System.Guid.NewGuid ().ToString ());
//			
//			SH.StartCoroutine (PoolServer ());
//			//m_client.Connect ();
		}
		
		//private bool m_joiningRoom;

		private void OnJoinedLobby()
		{
			LogService.Debug("PhotonMessenger.OnJoinedLobby");
			OnConnected();
		}

//		private IEnumerator PoolServer ()
//		{
//			while (true) {
//				/m_client.Service ();
//				LogService.Debug ("State: {0}", m_client.State);
//				
//				if (State == MessengerState.Disconnected) {
//					
//					switch (m_client.State) {
//					case ClientState.JoinedLobby:
//						//if (!m_joiningRoom) {
//							//m_joiningRoom = true;
//							
////							if (m_isServer) {
////								m_client.CreateRoom ("TESTE");
////							}	
//							m_client.OpJoinRandomRoom (null, 0);
//						//}
//						
//						break;
//						
//					case ClientState.Joined:
//						OnConnected ();
//						break;
//					}
//						
//				}
//				
//				yield return new WaitForSeconds(1f);
//			}
//		}

		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, object value)
		{
			Hashtable evData = new Hashtable ();
			evData [(byte)1] = name;
			evData [(byte)2] = value;
			//m_client.loadBalancingPeer.OpRaiseEvent(1, evData, true, 0);
		}

		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect ()
		{
			//m_client.Disconnect();
		}
		#endregion
	}
}