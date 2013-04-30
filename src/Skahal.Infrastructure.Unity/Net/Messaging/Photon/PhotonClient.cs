//#region Usings
//using System;
//using ExitGames.Client.Photon;
//using Skahal.Infrastructure.Framework.Commons;
//using Skahal.Infrastructure.Framework.Net.Messaging;
//using UnityEngine;
//#endregion
//
//namespace Skahal.Infrastructure.Unity.Net.Messaging.Photon
//{
//	/// <summary>
//	/// The Photon client.
//	/// </summary>
//	public class PhotonClient //: LoadBalancingClient
//	{
//		#region Events
//		/// <summary>
//		/// Occurs when message received.
//		/// </summary>
//		public event EventHandler<MessageReceivedEventArgs> MessageReceived;
//
//		/// <summary>
//		/// Occurs when status changed.
//		/// </summary>
//		public event EventHandler<PhotonStatusChangedEventArgs> StatusChanged;
//		#endregion
//		
//		#region Methods
//		/// <summary>
//		/// OnStatusChanged is called to let the game know when asyncronous actions finished or when errors happen.
//		/// </summary>
//		/// <remarks>Not all of the many StatusCode values will apply to your game. Example: If you don't use encryption, 
//		///  the respective status changes are never made.
//		///  
//		///  The values are all part of the StatusCode enumeration and described value-by-value.</remarks>
//		/// <param name="statusCode">A code to identify the situation.</param>
//		public override void OnStatusChanged (StatusCode statusCode)
//		{
//			base.OnStatusChanged (statusCode);
//			StatusChanged.Raise (this, new PhotonStatusChangedEventArgs (statusCode));
//		}
//
//		/// <summary>
//		/// Called whenever an event from the Photon Server is dispatched.
//		/// </summary>
//		/// <param name="photonEvent">Photon event.</param>
//		public override void OnEvent (EventData photonEvent)
//		{	
//			base.OnEvent (photonEvent);
//			
//			if (photonEvent.Code == (byte)1) {
//				var msg = new Message ();
//				msg.Name = photonEvent.Parameters [(byte)1].ToString ();
//				msg.Value = photonEvent.Parameters [(byte)2].ToString ();
//				MessageReceived.Raise (this, new MessageReceivedEventArgs (msg));	
//			}
//		
//		}
//		
//		#endregion
//	}
//}