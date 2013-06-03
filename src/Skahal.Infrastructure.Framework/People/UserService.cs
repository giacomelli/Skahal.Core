using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Logging;

namespace Skahal.Infrastructure.Framework.People
{
	/// <summary>
	/// Infrastructure framework user service.
	/// </summary>
	public static class UserService
	{
		#region Fields
		private static IUserRepository s_repository;
		private static User s_currentUser;
		#endregion

		#region Methods
		/// <summary>
		/// Initialize the services.
		/// </summary>
		/// <param name="userRepository">User repository.</param>
		public static void Initialize (IUserRepository userRepository)
		{
			s_repository = userRepository;
		}

	 	/// <summary>
		/// Gets the current user.
		/// </summary>
		/// <returns>The current user.</returns>
		public static User GetCurrentUser()
		{
			if(s_currentUser == null)
			{
				LogService.Debug("GetCurrentUser: there is no current user. Looking for the first one available on repository...");
				s_currentUser = s_repository.FindAll ((u) => true).FirstOrDefault();

				if(s_currentUser == null)
				{
					LogService.Debug("GetCurrentUser: there is no users on repository. Creating the first one and marking as current..");
					s_currentUser = new User();
					s_currentUser.Id = 1;
					s_currentUser.Name = "Default user";
					s_repository.Create(s_currentUser);
				}
				else {
					LogService.Debug("GetCurrentUser: first user found on repository: {0}-{1}.", s_currentUser.Id, s_currentUser.Name);
				}
			}

			return s_currentUser;
		}

		/// <summary>
		/// Sets the current user.
		/// </summary>
		/// <param name="user">User.</param>
		public static void SaveCurrentUser(User user)
		{
			s_currentUser = user;

			var oldUser = s_repository.FindAll(u => u.Id == user.Id).FirstOrDefault();
			
			if(oldUser == null)
			{
				LogService.Debug("SaveCurrentUser: creating the current user: {0}-{1}", user.Id, user.Name);
				s_repository.Create(user);
			}
			else 
			{
				LogService.Debug("SaveCurrentUser: updating the current user: {0}-{1}", user.Id, user.Name);
				s_repository.Modify(user);
			}
		}
		#endregion
	}
}

