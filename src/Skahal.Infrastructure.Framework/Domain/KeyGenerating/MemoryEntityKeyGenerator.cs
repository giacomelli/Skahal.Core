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
	/// <summary>
	/// An in-memory entity key generator.
	/// </summary>
	public class MemoryEntityKeyGenerator : IEntityKeyGenerator
	{
		#region Fields
		private Dictionary<Type, long> m_lastKeys = new Dictionary<Type, long>();
		#endregion
		
		#region ISHKeyGenerator implementation
		/// <summary>
		/// Gets the next key for entity type specified.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		public long NextKey(Type entityType)
		{
			lock(this)
			{
				if(!m_lastKeys.ContainsKey(entityType))
				{
					var keys = EntityIdentityMap.GetKeys(entityType);
					
					if(keys.Length == 0)
					{
						m_lastKeys.Add(entityType, 0);
					}
					else
					{
						Array.Sort(keys);
						m_lastKeys.Add(entityType, keys[keys.Length - 1]);
					}
				}
				
				var key = ++m_lastKeys[entityType];
				
				LogService.Debug("MemoryEntityKeyGenerator returning key {0} for entity {1}.", key, entityType);
				
				return key;
			}
		}

		/// <summary>
		/// Uses the key.
		/// </summary>
		/// <param name="entityType">Entity type.</param>
		/// <param name="key">Key.</param>
		public void UseKey(Type entityType, long key)
		{
			m_lastKeys.Add(entityType, key);
		}

		private void PrepareKeysForType(Type entityType)
		{
			if(!m_lastKeys.ContainsKey(entityType))
			{
				var keys = EntityIdentityMap.GetKeys(entityType);

				if(keys.Length == 0)
				{
					m_lastKeys.Add(entityType, 0);
				}
				else
				{
					Array.Sort(keys);
					m_lastKeys.Add(entityType, keys[keys.Length - 1]);
				}
			}
		}
		#endregion
	}
}