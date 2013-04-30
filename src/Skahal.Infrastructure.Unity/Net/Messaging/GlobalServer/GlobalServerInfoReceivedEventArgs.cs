#region Usings
using System;
using System.Collections;
using UnityEngine;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// Global server info received event arguments.
	/// </summary>
	public class GlobalServerInfoReceivedEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer.GlobalServerInfoReceivedEventArgs"/> class.
		/// </summary>
		/// <param name="availablePlayersCount">Available players count.</param>
		/// <param name="gamesCount">Games count.</param>
		/// <param name="isOnline">If set to <c>true</c> is online.</param>
		public GlobalServerInfoReceivedEventArgs (int availablePlayersCount, int gamesCount, bool isOnline)
		{
			AvailablePlayersCount = availablePlayersCount;
			GamesCount = gamesCount;
			IsOnline = isOnline;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the available players count.
		/// </summary>
		/// <value>The available players count.</value>
		public int AvailablePlayersCount { get; private set; }

		/// <summary>
		/// Gets the games count.
		/// </summary>
		/// <value>The games count.</value>
		public int GamesCount { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is online.
		/// </summary>
		/// <value><c>true</c> if this instance is online; otherwise, <c>false</c>.</value>
		public bool IsOnline { get; private set; }
		#endregion
	}
}