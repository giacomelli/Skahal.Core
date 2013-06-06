//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
using MonoTouch.GameKit;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	/// <summary>
	/// The Game Center achievement view controller delegate.
	/// </summary>
	public class SHGameCenterAchievementViewControllerDelegate : GKAchievementViewControllerDelegate
	{
		#region Methods
		/// <summary>
		/// Called to finish the view controller.
		/// </summary>
		/// <param name='viewController'>
		/// View controller.
		/// </param>
		public override void DidFinish (GKAchievementViewController viewController)
		{
			viewController.DismissModalViewControllerAnimated(true);
		}
		#endregion
	}
}

