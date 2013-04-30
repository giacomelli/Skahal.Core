#region Usings
using System;
using System.Collections;
using UnityEngine;
using Skahal.Domain;


#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer
{
	/// <summary>
	/// The  Global server player.
	/// </summary>
	public class GlobalServerPlayer : Player
	{
		/// <summary>
		/// Gets or sets the IP.
		/// </summary>
		/// <value>The I.</value>
		public string IP { get; set; }

		/// <summary>
		/// Gets or sets the device.
		/// </summary>
		/// <value>The device.</value>
		public string Device { get; set; }
	}
}