//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios
using Skahal.Infrastructure.Framework.Logging;

#region Usings
using System;
using System.Linq;
using System.Collections.Generic;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	public class MemoryEntityKeyGenerator : IEntityKeyGenerator
	{
		#region Fields
		private Dictionary<Type, long> s_lastKeys = new Dictionary<Type, long>();
		#endregion
		
		#region ISHKeyGenerator implementation
		public long NextKey(Type entityType)
		{
			lock(this)
			{
				if(!s_lastKeys.ContainsKey(entityType))
				{
					var keys = EntityIdentityMap.GetKeys(entityType);
					
					if(keys.Length == 0)
					{
						s_lastKeys.Add(entityType, 0);
					}
					else
					{
						Array.Sort(keys);
						s_lastKeys.Add(entityType, keys[keys.Length - 1]);
					}
				}
				
				var key = ++s_lastKeys[entityType];
				
				LogService.Debug("MemoryEntityKeyGenerator returning key {0} for entity {1}.", key, entityType);
				
				return key;
			}
		}
		#endregion
	}
}