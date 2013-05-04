using System;
using Skahal.Infrastructure.Framework.People;

namespace Skahal.Domain.People
{
	/// <summary>
	/// Domain player service.
	/// </summary>
	public static class PlayerService
	{
		#region Methods
		/// <summary>
		/// Gets the current player.
		/// </summary>
		/// <returns>The current player.</returns>
		public static Player GetCurrentPlayer()
		{
			var user = UserService.GetCurrentUser();

			return new Player(user);
		}
		#endregion
	}
}