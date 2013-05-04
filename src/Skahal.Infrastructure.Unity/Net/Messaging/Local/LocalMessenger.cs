#region Usings
using Skahal.Infrastructure.Framework.Net.Messaging;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Lan
{
	/// <summary>
	/// A local IMessenger implementation.
	/// </summary>
	public class LocalMessenger : MessengerBase
	{
		#region IRemoteMessenger implementation
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		public override void Connect ()
		{
			OnConnected(new ConnectedEventArgs(1));
		}

		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, string value)
		{
		}

		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect ()
		{
		}
		#endregion
	}
}