#region Usings
using System;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Commons;


#endregion

namespace Skahal.Infrastructure.Framework
{
	/// <summary>
	/// The framework bootstrapper.
	/// </summary>
	public abstract class BootstrapperBase
	{
		#region Properties
		/// <summary>
		/// Gets or sets the log strategy.
		/// </summary>
		/// <value>The log strategy.</value>
		public ILogStrategy LogStrategy { get; protected set; }
		#endregion

		#region Methods
		/// <summary>
		/// Setup this instance.
		/// </summary>
		public virtual void Setup()
		{
			LogService.Initialize(LogStrategy);
		}
		#endregion
	}
}