using System;
using System.Collections.Generic;
using Skahal.Domain.Devices;
using Skahal.Infrastructure.Framework.People;

namespace Skahal.Domain.People
{
	/// <summary>
	/// Represents a player.
	/// </summary>
	public class Player : User
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Domain.People.Player"/> class.
		/// </summary>
		public Player()
		{
			Devices = new List<Device>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Domain.People.Player"/> class.
		/// </summary>
		/// <param name="user">User.</param>
		public Player(User user) : base(user.Key)
		{
			RemoteKey = user.RemoteKey;
			Name = user.Name;
			Preferences = user.Preferences;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the devices.
		/// </summary>
		/// <value>The devices.</value>
		public IList<Device> Devices { get; private set; }
		#endregion
	}
}