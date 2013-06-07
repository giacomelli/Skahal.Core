//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	public static class EntityKeyGenerator
	{
		#region Fields
		private static IEntityKeyGenerator s_generator;
		#endregion
		
		#region Constructors
		static EntityKeyGenerator ()
		{
			s_generator = new MemoryEntityKeyGenerator();
		}
		#endregion
		
		#region Methods
		public static void Initialize(IEntityKeyGenerator keyGenerator)
		{
			s_generator = keyGenerator;
		}
		
		public static long NextKey(Type entityType) 
		{
			return s_generator.NextKey(entityType);
		}
		#endregion
	}
}

