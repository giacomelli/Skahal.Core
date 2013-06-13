using System;
using Skahal.Infrastructure.Framework.Repositories;

namespace Skahal.Infrastructure.Framework.Globalization
{
	/// <summary>
	/// An in-memory globalization label repository.
	/// </summary>
	public class MemoryGlobalizationLabelRepository : MemoryRepository<GlobalizationLabel, string>, IGlobalizationLabelRepository
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Framework.Globalization.MemoryGlobalizationLabelRepository"/> class.
		/// </summary>
		public MemoryGlobalizationLabelRepository () : base((l) => { return Guid.NewGuid().ToString(); })
		{
		}
		#endregion
	}
}

