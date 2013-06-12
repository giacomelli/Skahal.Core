//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios
using Skahal.Infrastructure.Framework.Logging;
using HelperSharp;

#region Usings
using System;
using System.Linq;
using System.Collections.Generic;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	/// <summary>
	/// An in-memory long entity key generator.
	/// </summary>
	public class MemoryLongEntityKeyGenerator : IEntityKeyGenerator<long>
	{
		#region Fields
		private Dictionary<Type, long> m_lastKeys = new Dictionary<Type, long>();
		#endregion

		#region Methods
		/// <summary>
		/// Sets the last key.
		/// </summary>
		/// <param name="entityType">Entity type.</param>
		/// <param name="lastKey">Last key.</param>
		public void SetLastKey(Type entityType, long lastKey)
		{
			m_lastKeys [entityType] = lastKey;
		}
		#endregion

		#region IEntityKeyGenerator implementation
		/// <summary>
		/// Gets the next key for entity type specified.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="entityType">Entity type.</param>
		public long NextKey(Type entityType)
		{
			lock(this)
			{
				Validate (entityType);			
				var key = ++m_lastKeys[entityType];
				UseKey (entityType, key);
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
			Validate (entityType);

			var currentKey = m_lastKeys [entityType];

			if(key > currentKey)
			{
				m_lastKeys [entityType] = key;
			}
		}

		private void Validate(Type entityType)
		{
			if (!m_lastKeys.ContainsKey (entityType)) {
				LogService.Warning("There is no last key for entity of type '{0}'. You should use SetLastKey on MemoryEntityKeyGenerator before. Using 0 (zero) has last key.".With(entityType));
				SetLastKey (entityType, 0);
			}
		}
		#endregion
	}
}