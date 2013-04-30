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
	public abstract class PlayerPrefsRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		#region Methods
		/// <summary>
		/// Get all entities.
		/// </summary>
		public virtual IQueryable<TEntity> All ()
		{
			var allIds = GetAllIds ();
			var result = new List<TEntity> ();
			
			foreach (var id in allIds) {
				if (!String.IsNullOrEmpty (id)) {
					var r = SerializationHelper.DeserializeFromString<TEntity> (PlayerPrefs.GetString(GetKey (long.Parse(id))));
					result.Add(r);
				}			
			}
			
			return result.AsQueryable();
		}

		/// <summary>
		/// Create the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual TEntity Create (TEntity entity)
		{
			entity.Id = GetLastId () + 1;
			
			Modify(entity);
			SetLastId (entity.Id);
			
			return entity;
		}

		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual void Delete(TEntity entity)
		{
			PlayerPrefs.DeleteKey(GetKey(entity.Id));
		}

		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public virtual void Delete (int id)
		{
			PlayerPrefs.DeleteKey (GetKey (id));
		}

		/// <summary>
		/// Modify the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual void Modify (TEntity entity)
		{
			var serialized = SerializationHelper.SerializeToString (entity);
			var key = GetKey (entity.Id);
			PlayerPrefs.SetString (key, serialized);
		}
		#endregion	
		
		#region Fields
		private string GetKey (long id)
		{
			return String.Format ("PlayerPrefsRepository_{0}_{1}", typeof(TEntity).Name, id);
		}
		
		private long GetLastId ()
		{
			var lastKey = PlayerPrefs.GetString (String.Format ("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), "0");
			
			return long.Parse (lastKey);
		}
		
		private void SetLastId (long id)
		{
			PlayerPrefs.SetString (String.Format ("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), id.ToString ());
			
			if (id > 1) {
				var currentIds = PlayerPrefs.GetString (GetAllIdsKey(), "");
				PlayerPrefs.SetString (GetAllIdsKey(), currentIds + "," + id);
			}
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