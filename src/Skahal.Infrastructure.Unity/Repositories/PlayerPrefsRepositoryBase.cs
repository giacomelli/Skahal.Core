#region Usings
using System;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using Skahal.Infrastructure.Unity.Serialization;
using System.Linq;
using System.Linq.Expressions;
#endregion

namespace Skahal.Infrastructure.Unity.Repositories
{
	/// <summary>
	/// Player prefs repository base.
	/// </summary>
	public abstract class PlayerPrefsRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class, IAggregateRoot
	{
		#region Methods
		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The entities found.</returns>
		public override IEnumerable<TEntity> FindAll(Func<TEntity, bool> filter)
		{
			var allIds = GetAllIds ();

			foreach (var id in allIds) {
				if (!String.IsNullOrEmpty (id)) {
					var entity = SerializationHelper.DeserializeFromString<TEntity> (PlayerPrefs.GetString(GetKey (long.Parse(id))));

					if(filter(entity))
					{
						yield return entity;
					}
				}			
			}
		}

		public override TEntity FindBy (long key)
		{
			return FindAll(f => f.Key == key).FirstOrDefault();
		}

		public override IEnumerable<TEntity> FindAll (int offset, int limit, Func<TEntity, bool> filter)
		{
			return FindAll (filter).Skip (offset).Take (limit);
		}

		public override int CountAll (Func<TEntity, bool> filter)
		{
			return FindAll (filter).Count ();
		}

		protected override void PersistNewItem (TEntity item)
		{
			var serialized = SerializationHelper.SerializeToString (item);
			var key = GetKey (item.Key);
			PlayerPrefs.SetString (key, serialized);
		}

		protected override void PersistUpdatedItem (TEntity item)
		{
			PersistNewItem (item);
		}

		protected override void PersistDeletedItem (TEntity item)
		{
			PlayerPrefs.DeleteKey(GetKey(item.Key));
		}

		#endregion
		
		#region Fields
		private string GetKey (long id)
		{
			return String.Format ("PlayerPrefsRepository_{0}_{1}", typeof(TEntity).Name, id);
		}
		
		private string GetAllIdsKey ()
		{
			return String.Format ("PlayerPrefsRepository_{0}__AllIds", typeof(TEntity).Name);	
		}
		
		private string[] GetAllIds()
		{
			return PlayerPrefs.GetString (GetAllIdsKey(), "").Split(',');
		}
		#endregion
	}
}