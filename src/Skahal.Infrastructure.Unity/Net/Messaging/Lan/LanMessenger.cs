#region Usings
using System;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
using Skahal.Infrastructure.Unity.Commons;
using Skahal.Infrastructure.Framework.Logging;


#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Lan
{
	/// <summary>
	/// A Lan IMessenger implementation.
	/// </summary>
	public class LanMessenger : MessengerBase
	{
		#region Fields
		private LanMessengerProxy m_proxy;
		private string m_serverIP;
		private int m_port;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Unity.Net.Messaging.Lan.LanMessenger"/> class.
		/// <remarks>Use this constructor for client peer.</remarks>
		/// </summary>
		/// <param name="serverIP">Server IP address.</param>
		/// <param name="port">Port.</param>
		public LanMessenger(string serverIP, int port)
		{
			m_serverIP = serverIP;
			m_port = port;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Unity.Net.Messaging.Lan.LanMessenger"/> class.
		/// <remarks>Use this constructor for server peer.</remarks>
		/// </summary>
		/// <param name="serverIP">Server IP address.</param>
		/// <param name="port">Port.</param>
		public LanMessenger(int port)
		{
			m_port = port;
		}
		#endregion
		
		#region IRemoteMessenger implementation
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		/// <param name="isServer">If set to <c>true</c> is server.</param>
		public override void Connect ()
		{
			GameObjectHelper.Destroy("LanMessengerProxy");
			m_proxy = new GameObject ("LanMessengerProxy").AddComponent<LanMessengerProxy> ();
			m_proxy.IsServer = string.IsNullOrEmpty(m_serverIP);

			LogService.Debug("LanMessenger: connecting as {0}...", m_proxy.IsServer ? "server" : "client");

			m_proxy.ServerIP = m_serverIP;
			m_proxy.Port = m_port;
			
			m_proxy.Connected += delegate {
				OnConnected (new ConnectedEventArgs(m_proxy.IsServer ? 1 : 2));
			};
			
			m_proxy.MessageReceived += delegate(object sender, MessageEventArgs e) {		
				OnMessageReceived (e);
			};
			
			m_proxy.Disconnected += delegate {
				OnDisconnected(new DisconnectedEventArgs (DisconnectionReason.ConnectionLost));
			};
		}
			
		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, string value)
		{
			m_proxy.SendMessageToPlayer (name, value);
		}

		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect()
		{
			Network.Disconnect ();
		}
		#endregion
	}
}