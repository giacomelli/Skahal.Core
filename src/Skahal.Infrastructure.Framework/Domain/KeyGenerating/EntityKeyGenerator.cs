//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

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
		private static IEntityKeyGenerator s_generator;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes the <see cref="Skahal.Infrastructure.Framework.Domain.KeyGenerating.EntityKeyGenerator"/> class.
		/// </summary>
		static EntityKeyGenerator ()
		{
			s_generator = new MemoryEntityKeyGenerator();
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Initialize the specified keyGenerator.
		/// </summary>
		/// <param name="keyGenerator">Key generator.</param>
		public static void Initialize(IEntityKeyGenerator keyGenerator)
		{
			s_generator = keyGenerator;
		}

		/// <summary>
		/// Gets the next key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		public static long NextKey(Type entityType) 
		{
			return s_generator.NextKey(entityType);
		}
		#endregion
	}
}

