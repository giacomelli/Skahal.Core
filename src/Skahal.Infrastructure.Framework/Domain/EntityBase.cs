#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Base class for entities.
	/// </summary>
	[Serializable]
	public abstract class EntityBase : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public long Id { get; set; }
		#endregion
	}
}
