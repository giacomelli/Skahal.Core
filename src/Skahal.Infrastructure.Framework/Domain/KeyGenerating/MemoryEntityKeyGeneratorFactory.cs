using System;

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	/// <summary>
	/// In-memory entity key generator factory.
	/// </summary>
	public class MemoryEntityKeyGeneratorFactory : IEntityKeyGeneratorFactory
	{
		#region IEntityKeyGeneratorFactory implementation
		/// <summary>
		/// Creates the int entity key generator.
		/// </summary>
		/// <returns>The int entity key generator.</returns>
		public IEntityKeyGenerator<int> CreateIntEntityKeyGenerator ()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Creates the long entity key generator.
		/// </summary>
		/// <returns>The long entity key generator.</returns>
		public IEntityKeyGenerator<long> CreateLongEntityKeyGenerator ()
		{
			return new MemoryLongEntityKeyGenerator();
		}

		/// <summary>
		/// Creates the GUID entity key generator.
		/// </summary>
		/// <returns>The GUID entity key generator.</returns>
		public IEntityKeyGenerator<Guid> CreateGuidEntityKeyGenerator ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

