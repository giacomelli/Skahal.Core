//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios
using System.Collections.Generic;

#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	/// <summary>
	/// Entity key generator.
	/// </summary>
	public static class EntityKeyGenerator
	{
		#region Fields
		private static Dictionary<Type, object> s_generators = new Dictionary<Type, object>();
		private static IEntityKeyGeneratorFactory s_factory = new MemoryEntityKeyGeneratorFactory ();
		#endregion

		#region Methods
		/// <summary>
		/// Initialize the specified factory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void Initialize(IEntityKeyGeneratorFactory factory)
		{
			s_factory = factory;
		}

		/// <summary>
		/// Gets the next key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		public static TKey NextKey<TKey>(Type entityType) 
		{
			return s_generator.NextKey(entityType);
		}

		/// <summary>
		/// Uses the key.
		/// </summary>
		/// <param name="entityType">Entity type.</param>
		/// <param name="key">Key.</param>
		public static void UseKey (Type entityType, long key)
		{
			s_generator.UseKey (entityType, key);
		}
		#endregion
	}
}

