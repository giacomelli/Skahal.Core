#region Usings
using System;
using System.Collections.Generic;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public interface ISGNMultiplayerManager
	{
		#region Events
		event EventHandler<OpponentConnectedEventArgs> OpponentConnected;
		event EventHandler<OpponentDisconnectedEventArgs> OpponentDisconnected;
		event EventHandler<InviteReceivedEventArgs> InviteReceived;
		event EventHandler<ActivityOpponentsRefreshedEventArgs> ActivityOpponentsRefreshed;
		#endregion
		
		#region Properties
		bool Supported { get; }
		bool IsHost { get; }
		IList<SGNPlayer> Opponents { get; }
		#endregion
	
		#region Methods
		void Initialize();
		void SendMessage(string messageName, string messageValue);
		void CloseSession();
		void ShowAvailableOpponents();
		void RefreshActivityOpponents();
		#endregion
	}
}