//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
using MonoTouch.GameKit;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// The Game Center leaderboard view controller.
	/// </summary>
	public class SHGameCenterLeaderboardViewControllerDelegate : GKLeaderboardViewControllerDelegate
	{
		#region Methods
		/// <summary>
		/// Called to finish the view controller.
		/// </summary>
		/// <param name='viewController'>
		/// View controller.
		/// </param>
		public override void DidFinish (GKLeaderboardViewController viewController)
		{
			viewController.DismissModalViewControllerAnimated(true);
		}
		#endregion
	}
}

