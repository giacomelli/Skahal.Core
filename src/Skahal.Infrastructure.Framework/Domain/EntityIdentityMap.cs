//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Skahal.Infrastructure.Framework.Domain
{
	public static class EntityIdentityMap
	{
		#region Fields
		private static Dictionary<Type, Dictionary<long, EntityBase>>  s_maps = new Dictionary<Type, Dictionary<long, EntityBase>>();
		#endregion
		
		#region Public Methods
		public static void Add<TEntity>(TEntity entity) 
			where TEntity: EntityBase
		{
			var entityType = entity.GetType();
			
			// There is a map for this type?
			if(!s_maps.ContainsKey(entityType))
			{
				s_maps.Add(entityType, new Dictionary<long, EntityBase>());
			}
			
			// Gets the map for the type.
			var map = s_maps[entityType];
			var entityKey = entity.Key;
			
			// Can Add the entity with the key in the map?
			if(!map.ContainsKey(entityKey))
			{
				map.Add(entityKey, entity);
			}
		}
		
		public static TEntity Get<TEntity>(int entityKey)
			where TEntity: EntityBase
		{
			var entityType = typeof(TEntity);
			TEntity entity = null;
			
			if(s_maps.ContainsKey(entityType))
			{
				var map = s_maps[entityType];
				
				if(map.ContainsKey(entityKey))
				{
					entity = (TEntity)map[entityKey];	
				}
			}
			
			return entity;
		}
		
		internal static long[] GetKeys(Type entityType) 
		{
			var keys = new long[0];
			
			if(s_maps.ContainsKey(entityType))
			{
				keys =  s_maps[entityType].Keys.ToArray();
			}
			
			return keys;
		}
		#endregion
	}
}

