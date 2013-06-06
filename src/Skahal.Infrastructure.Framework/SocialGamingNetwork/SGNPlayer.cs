#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public class SGNPlayer
	{
		#region Constructors
		public SGNPlayer(string id)
		{
			ID = id;
		}
		#endregion
		
		#region Properties
		public string ID { get; private set;}
		public string Name { get; set;}
		#endregion
		
		#region Methods
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
		
		public override string ToString()
		{
			return string.Format("[SGNPlayer: ID={0}, Name={1}]", ID, Name);
		}
		#endregion
	}
}