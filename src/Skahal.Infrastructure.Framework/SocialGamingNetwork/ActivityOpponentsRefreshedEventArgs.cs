#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class ActivityOpponentsRefreshedEventArgs : EventArgs
	{
		#region Constructors
		public ActivityOpponentsRefreshedEventArgs(int activityOpponentsCount)
		{
			ActivityOpponentsCount = activityOpponentsCount;
		}
		#endregion
		
		#region Properties
		public int ActivityOpponentsCount
		{
			get;
			private set;
		}
		#endregion
	}
}