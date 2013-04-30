#region Usings
using System;
using System.Collections;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{	
	/// <summary>
	/// An authoritative server IGlobalServerMessagingStrategy implementation. 
	/// </summary>
	public class GlobalServerAuthoritativeServerMessagingStrategy : IGlobalServerMessagingStrategy
	{
		#region Fields
		private GlobalServerClient m_client;
		private string m_enemyId;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer.GlobalServerAuthoritativeServerMessagingStrategy"/> class.
		/// </summary>
		/// <param name="client">Client.</param>
		public GlobalServerAuthoritativeServerMessagingStrategy(GlobalServerClient client)
		{
			m_client = client;	
		}
		#endregion

		#region IGlobalServerMessagingStrategy implementation
		/// <summary>
		/// Occurs when message received.
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageReceived;

		/// <summary>
		/// Occurs when peer disconnected.
		/// </summary>
		public event EventHandler PeerDisconnected;

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize()
		{
			
		}

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public void SendMessage (string name, string value)
		{
			m_client.Request (
				"Multiplayer/EnqueueMessage", 
				"playerId", m_enemyId, 
				"messageName", name,
				"messageValue", value);
		}

		/// <summary>
		/// Receives the message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public void ReceiveMessage (string name, object value)
		{
			switch (name)
			{
			// Cannot pause, because Time.timeScale goes to zero, then messages can't be pulled from ther server.	
			case "OnEnemyPaused":
			case "OnEnemyResumed":
				break;
			
			case "GameEnded":
				if (value.Equals ("OnEnemyQuit"))
				{
					MessageReceived.Raise (this, new MessageEventArgs ("OnEnemyQuit", null));
				}
				
				PeerDisconnected.Raise (this);
				
				break;
				
			default:
				MessageReceived.Raise (this, new MessageEventArgs (name, value));
				break;
			}
		}

		/// <summary>
		/// Connect the specified isHost and enemy.
		/// </summary>
		/// <param name="isHost">If set to <c>true</c> is host.</param>
		/// <param name="enemy">Enemy.</param>
		public bool Connect (bool isHost, GlobalServerPlayer enemy)
		{
			m_enemyId = enemy.Id;
			return false;
		}

		/// <summary>
		/// Disconnect this instance.
		/// </summary>
		public void Disconnect()
		{
			
		}

		/// <summary>
		/// Gets the player address.
		/// </summary>
		/// <value>The player address.</value>
		public string PlayerAddress {
			get {
				return String.Empty;
			}
		}
		#endregion
	}
}