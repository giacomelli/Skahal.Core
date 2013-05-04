#region Usings
using Skahal.Infrastructure.Framework.Net.Messaging;
using Skahal.Domain.People;


#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// A messenger using the Global Server tech.
	/// </summary>
	public class GlobalServerMessenger : MessengerBase
	{
		#region Fields
		private GlobalServerClient m_client;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer.GlobalServerMessenger"/> class.
		/// </summary>
		/// <param name="serverAddress">Server address.</param>
		/// <param name="multiplayerVersion">Multiplayer version.</param>
		public GlobalServerMessenger (Player player, string serverAddress, string multiplayerVersion)
		{
			m_client = new GlobalServerClient (player, serverAddress, multiplayerVersion);
			
			m_client.GameCreated += delegate(object sender, GlobalServerGameCreatedEventArgs e) {
				OnConnected (new ConnectedEventArgs(e.IsHost ? 1 : 0));
			};

			m_client.MessageReceived += delegate(object sender, MessageEventArgs e) {
				OnMessageReceived (e);
			};
			
			m_client.PeerDisconnected += delegate {
				OnDisconnected (new DisconnectedEventArgs (DisconnectionReason.ConnectionLost));
			};
		}
		#endregion
		
		#region implemented abstract members of Skahal.Infrastructure.Framework.Net.Messaging.MessengerBase
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		public override void Connect ()
		{
			m_client.Connect ();
			m_client.CreateGame();
		}

		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage(string name, string value)
		{
			m_client.SendMessage(name, value);
		}

		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect()
		{
			m_client.Disconnect();
		}
		#endregion
	}
}