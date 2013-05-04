#region Usings
using UnityEngine;
using System.Collections;
using System;
using Skahal.Infrastructure.Framework.Net.Messaging;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// Defines a interface for messaging on Global Server.
	/// </summary>
	public interface IGlobalServerMessagingStrategy
	{
		#region Events
		/// <summary>
		/// Occurs when message received.
		/// </summary>
		event EventHandler<MessageEventArgs> MessageReceived;

		/// <summary>
		/// Occurs when peer disconnected.
		/// </summary>
		event EventHandler PeerDisconnected;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the player address.
		/// </summary>
		/// <value>The player address.</value>
		string PlayerAddress { get; }
		#endregion

		#region Methods
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		void SendMessage(string name, string value);

		/// <summary>
		/// Receives the message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		void ReceiveMessage(string name, string value);

		/// <summary>
		/// Connect the specified isHost and enemy.
		/// </summary>
		/// <param name="isHost">If set to <c>true</c> is host.</param>
		/// <param name="enemy">Enemy.</param>
		bool Connect (bool isHost, GlobalServerPlayer enemy);

		/// <summary>
		/// Disconnect this instance.
		/// </summary>
		void Disconnect();
		#endregion
	}
}