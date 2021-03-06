using System;
using Skahal.Infrastructure.Framework;
using Skahal.Infrastructure.Unity.Logging;
using Skahal.Infrastructure.Unity.People;
using Skahal.Infrastructure.Framework.Configuration;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Unity.Globalization;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Globalization;
using System.Linq;

namespace Skahal.Infrastructure.Unity.Configuration
{
	/// <summary>
	/// Unity bootstrapper.
	/// </summary>
	public class UnityBootstrapper : BootstrapperBase
	{
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether the bootstrapper should be configured for debug purposes.
		/// </summary>
		/// <value><c>true</c> if this instance is debug; otherwise, <c>false</c>.</value>
		public bool IsDebug { get; set; }
		#endregion

		#region implemented abstract members of BootstrapperBase
		/// <summary>
		/// Creates the log strategy.
		/// </summary>
		/// <returns>The log strategy.</returns>
		protected override ILogStrategy CreateLogStrategy ()
		{
			if(IsDebug)
			{
				return new DebugLogStrategy();
			}
			else 
			{
				return new ReleaseLogStrategy();
			}
		}

		/// <summary>
		/// Creates the user repository.
		/// </summary>
		/// <returns>The user repository.</returns>
		protected override IUserRepository CreateUserRepository ()
		{
			return new ProtobufUserRepository();
		}

		/// <summary>
		/// Creates the app strategy.
		/// </summary>
		/// <returns>The app strategy.</returns>
		protected override IAppStrategy CreateAppStrategy ()
		{
			return null;
		}

		/// <summary>
		/// Creates the globalization label repository.
		/// </summary>
		/// <returns>The globalization label repository.</returns>
		protected override IGlobalizationLabelRepository CreateGlobalizationLabelRepository ()
		{
			return new GlobalizationLabelRepository ();
		}
		#endregion
	}
}