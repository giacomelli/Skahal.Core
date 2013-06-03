#region Usings
using System;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.People;


#endregion

namespace Skahal.Infrastructure.Framework.Configuration
{
	/// <summary>
	/// The framework bootstrapper.
	/// </summary>
	public abstract class BootstrapperBase
	{
		#region Fields
		private static bool s_alreadyBooted;
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the log strategy.
		/// </summary>
		/// <value>The log strategy.</value>
		public ILogStrategy LogStrategy { get; protected set; }

		/// <summary>
		/// Gets or sets the user repository.
		/// </summary>
		/// <value>The user repository.</value>
		public IUserRepository UserRepository { get; protected set; }
		#endregion

		#region Methods
		/// <summary>
		/// Fills the setup properties.
		/// </summary>
		protected abstract void FillSetupProperties();

		/// <summary>
		/// Setup this instance.
		/// </summary>
		public bool Setup()
		{
			if(!s_alreadyBooted)
			{	
				LogService.Debug("Bootstrapper '{0}' setup...", GetType().Name);

				FillSetupProperties();

				if (LogStrategy == null) {
					LogService.Warning ("LogStrategy not defined on bootstrapper.");
				} else {
					LogService.Debug ("'{0}' as LogStrategy.", LogStrategy.GetType ().Name);
					LogService.Initialize (LogStrategy);
				}

				if (UserRepository == null) {
					LogService.Warning ("UserRepository not defined on bootstrapper.");
				} else {
					LogService.Debug ("'{0}' as UserRepository.", UserRepository.GetType ().Name);
					UserService.Initialize (UserRepository);
				}

				s_alreadyBooted = true;
				LogService.Debug("Bootstrapper '{0}' setup done.", GetType().Name);

				return true;
			}

			return false;
		}
		#endregion
	}
}