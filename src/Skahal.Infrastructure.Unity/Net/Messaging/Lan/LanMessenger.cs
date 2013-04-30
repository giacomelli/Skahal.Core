#region Usings
using System;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
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
		#endregion
		
		#region IRemoteMessenger implementation
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		/// <param name="isServer">If set to <c>true</c> is server.</param>
		public override void Connect (bool isServer)
		{
			m_proxy = new GameObject ("LanMessengerProxy").AddComponent<LanMessengerProxy> ();
			m_proxy.IsServer = isServer;
			
			m_proxy.Connected += delegate {
				OnConnected ();
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
		internal protected override void PerformSendMessage (string name, object value)
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