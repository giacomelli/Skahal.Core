#region Usings
using Skahal.Infrastructure.Framework.Net.Messaging;
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
		public GlobalServerMessenger (string serverAddress, string multiplayerVersion)
		{
			m_client = new GlobalServerClient (serverAddress, multiplayerVersion);
			
			m_client.GameCreated += delegate(object sender, GlobalServerGameCreatedEventArgs e) {
				OnConnected ();
			};
			
			m_client.MessageReceived += delegate(object sender, MessageEventArgs e) {
				OnMessageReceived (e);
			};
			
			m_client.PeerDisconnected += delegate {
				OnDisconnected (new DisconnectedEventArgs (DisconnectionReason.RemoteQuit));
			};
		}
		#endregion
		
		#region implemented abstract members of Skahal.Infrastructure.Framework.Net.Messaging.MessengerBase
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		/// <param name="isServer">If set to <c>true</c> is server.</param>
		public override void Connect (bool isServer)
		{
			m_client.Connect ();
			m_client.CreateGame();
		}

		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage(string name, object value)
		{
			m_client.SendMessage(name, System.Convert.ToString(value));
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