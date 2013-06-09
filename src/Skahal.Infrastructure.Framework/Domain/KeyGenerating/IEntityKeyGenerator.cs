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
	public interface IEntityKeyGenerator
	{
		#region Usings
		/// <summary>
		/// Gets the next key for entity type specified.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		long NextKey(Type entityType);
		#endregion
	}
}

