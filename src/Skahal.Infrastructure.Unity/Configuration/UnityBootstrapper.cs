using System;
using Skahal.Infrastructure.Framework;
using Skahal.Infrastructure.Unity.Logging;
using Skahal.Infrastructure.Unity.People;
using Skahal.Infrastructure.Framework.Configuration;

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

		#region Methods
		/// <summary>
		/// Fills the setup properties.
		/// </summary>
		protected override void FillSetupProperties ()
		{
			if(IsDebug)
			{
				LogStrategy = new DebugLogStrategy();
			}
			else 
			{
				LogStrategy = new ReleaseLogStrategy();
			}

			UserRepository = new ProtobufUserRepository();
		}
		#endregion
	}
}