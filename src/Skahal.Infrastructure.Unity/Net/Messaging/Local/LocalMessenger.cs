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
		/// <param name="isServer">If set to <c>true</c> is server.</param>
		public override void Connect (bool isServer)
		{
			OnConnected();
		}

		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, object value)
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