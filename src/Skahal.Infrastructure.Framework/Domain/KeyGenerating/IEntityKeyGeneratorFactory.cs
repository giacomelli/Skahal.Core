using System;

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	/// <summary>
	/// Defines a interface for a entity key generator factory.
	/// </summary>
	public interface IEntityKeyGeneratorFactory
	{
		#region Methods
		/// <summary>
		/// Creates the int entity key generator.
		/// </summary>
		/// <returns>The int entity key generator.</returns>
		IEntityKeyGenerator<int> CreateIntEntityKeyGenerator();

		/// <summary>
		/// Creates the long entity key generator.
		/// </summary>
		/// <returns>The long entity key generator.</returns>
		IEntityKeyGenerator<long> CreateLongEntityKeyGenerator();

		/// <summary>
		/// Creates the GUID entity key generator.
		/// </summary>
		/// <returns>The GUID entity key generator.</returns>
		IEntityKeyGenerator<Guid> CreateGuidEntityKeyGenerator();
		#endregion
	}
}

