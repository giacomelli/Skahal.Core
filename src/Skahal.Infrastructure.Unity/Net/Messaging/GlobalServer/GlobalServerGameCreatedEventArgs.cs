#region Usings
using System;
using System.Collections;
using UnityEngine;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// Global server game created event arguments.
	/// </summary>
	public class GlobalServerGameCreatedEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer.GlobalServerGameCreatedEventArgs"/> class.
		/// </summary>
		/// <param name="isHost">If set to <c>true</c> is host.</param>
		/// <param name="enemy">Enemy.</param>
		public GlobalServerGameCreatedEventArgs (bool isHost, GlobalServerPlayer enemy)
		{
			IsHost = isHost;
			Enemy = enemy;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets a value indicating whether this instance is host.
		/// </summary>
		/// <value><c>true</c> if this instance is host; otherwise, <c>false</c>.</value>
		public bool IsHost { get; private set; }

		/// <summary>
		/// Gets the enemy.
		/// </summary>
		/// <value>The enemy.</value>
		public GlobalServerPlayer Enemy { get; private set; }
		#endregion
	}
}