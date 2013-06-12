//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	/// <summary>
	/// Defines a interface for an entity key generator.
	/// </summary>
	public interface IEntityKeyGenerator<TKey>
	{
		#region Usings
		/// <summary>
		/// Gets the next key for entity type specified.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		TKey NextKey(Type entityType);

		/// <summary>
		/// Uses the key.
		/// </summary>
		/// <param name="entityType">Entity type.</param>
		/// <param name="key">Key.</param>
		void UseKey (Type entityType, TKey key);
		#endregion
	}
}

