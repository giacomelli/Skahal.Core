#region Usings
using System.Collections;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Bluetooth
{
	/// <summary>
	/// A Bluetooth IMessenger implementation.
	/// </summary>
	public class BluetoothMessenger : MessengerBase 
	{
		#region Constants
		/// <summary>
		/// The name of the proxy game object.
		/// </summary>
		private const string ProxyGameObjectName = "BluetoothManagerProxy";
		#endregion
			
		#region Fields
		private BluetoothMessengerProxy m_proxy;
		#endregion
		
		#region Methods
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		public override void Connect ()
		{
			m_proxy = new GameObject (ProxyGameObjectName).AddComponent<BluetoothMessengerProxy> ();
			
			m_proxy.Connected += delegate {
				OnConnected (new ConnectedEventArgs(BluetoothManager.IsLeader() ? 1 : 2));
			};
			
			m_proxy.MessageReceived += delegate(object sender, MessageEventArgs e) {
				OnMessageReceived (e);
			};
			
			m_proxy.Disconnected += delegate {
				OnDisconnected(new DisconnectedEventArgs(DisconnectionReason.ConnectionLost));
			};
			
			BluetoothManager.ShowPicker ();
		}
		
		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, string value)
		{
			BluetoothManager.SendMessage (
				ProxyGameObjectName, 
				"OnMessageReceived", 
				MessageConverter.ToString (name, value),
				true);
		}
	
		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect()
		{
			BluetoothManager.CloseSession ();
		}
		#endregion
	}
}