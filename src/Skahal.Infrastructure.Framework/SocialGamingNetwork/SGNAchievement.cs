#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class SGNAchievement
	{
		#region Constructors
		public SGNAchievement(string id)
		{
			ID = id;
		}
		#endregion
		
		#region Properties
		public string ID { get; private set;}
		public string Name { get; set; }
		public float Percent { get; set; }
		#endregion
		
		#region Methods
		public override bool Equals(object obj)
		{
			var achievement = obj as SGNAchievement;
			
			if (obj == null)
			{
				return false;
			}
			
			return achievement.ID.Equals(ID);
		}
	
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
		
		public override string ToString()
		{
			return string.Format("[SGNAchievement: ID={0}, Name={1}, Percent={2}]", ID, Name, Percent);
		}
		#endregion
	}
}