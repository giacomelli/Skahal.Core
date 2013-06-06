#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class SGNLeaderboard
	{
		#region Constructors
		public SGNLeaderboard(string id)
		{
			ID = id;
		}
		#endregion
		
		#region Properties
		public string ID { get; private set;}
		public string Name { get; set; }
		public long Score { get; set; }
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			var leaderboad = obj as SGNLeaderboard;
			
			if (obj == null)
			{
				return false;
			}
			
			return leaderboad.ID.Equals(ID);
		}
		
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
		
		public override string ToString()
		{
			return string.Format("[SGNLeaderboard: ID={0}, Name={1}, Score={2}]", ID, Name, Score);
		}
		#endregion
	}
}