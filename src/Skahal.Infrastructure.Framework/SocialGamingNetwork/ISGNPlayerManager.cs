#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.SocialGamingNetwork
{
	public interface ISGNPlayerManager
	{
		#region Events
		event EventHandler<PlayerLoggedInEventArgs> LoggedIn;
		#endregion
		
		#region Properties
		SGNPlayer Player { get;}
		bool IsLogged {get;}
		#endregion
		
		#region Methods
		void Login();
		#endregion
	}
}