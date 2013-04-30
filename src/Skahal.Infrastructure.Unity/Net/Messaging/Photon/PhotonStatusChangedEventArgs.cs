#region Usings
using System;
using ExitGames.Client.Photon;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Photon
{
	/// <summary>
	/// Photon status changed event arguments.
	/// </summary>
	public class PhotonStatusChangedEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.Photon.PhotonStatusChangedEventArgs"/> class.
		/// </summary>
		/// <param name="statusCode">Status code.</param>
		public PhotonStatusChangedEventArgs (StatusCode statusCode)
		{
			StatusCode = statusCode;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the status code.
		/// </summary>
		/// <value>The status code.</value>
		public StatusCode StatusCode { get; private set; }
		#endregion
	}
}