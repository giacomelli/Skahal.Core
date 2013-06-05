using System;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;
using System.Linq;
using HelperSharp;

namespace Skahal.Infrastructure.Framework.Repositories
{
	/// <summary>
	/// A basic repository on memory.
	/// <remarks>
	/// In most of cases will be used for tests purposes.
	/// </remarks>
	/// </summary>
	public class MemoryRepository<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
	{
		#region Fields
		private List<TEntity> m_entities = new List<TEntity> ();
		private int m_nextId = 1;
		#endregion

		#region IRepository implementation
		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The entities found.</returns>
		/// <param name="filter">Filter.</param>
		public IEnumerable<TEntity> FindAll (Func<TEntity, bool> filter)
		{
			ExceptionHelper.ThrowIfNull ("filter", filter);

			return m_entities.Where (e => filter(e)).OrderBy(e => e.Id);
		}

		/// <summary>
		/// Counts all entities.
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="filter">Filter.</param>
		public long CountAll (Func<TEntity, bool> filter)
		{
			ExceptionHelper.ThrowIfNull ("filter", filter);
			return m_entities.Count (e => filter(e));
		}


		/// <summary>
		/// Create the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public TEntity Create (TEntity entity)
		{
			ExceptionHelper.ThrowIfNull ("entity", entity);

			if (m_entities.FirstOrDefault (e => e.Id == entity.Id) != null) {
				throw new InvalidOperationException ("There is another entity with id '{0}'.".With(entity.Id));
			}

			if (entity.Id == 0) {
				entity.Id = m_nextId++;
			}

			m_entities.Add (entity);

			return entity;
		}

		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public void Delete (TEntity entity)
		{
			ExceptionHelper.ThrowIfNull ("entity", entity);

			var old = m_entities.FirstOrDefault (e => e.Id == entity.Id);

			if (old == null) {
				throw new InvalidOperationException ("There is no entity with id '{0}'.".With(entity.Id));
			}

			m_entities.Remove (old);
		}

		/// <summary>
		/// Modify the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public void Modify (TEntity entity)
		{
			ExceptionHelper.ThrowIfNull ("entity", entity);
	
			Delete (entity);
			Create (entity);
		}

		#endregion
	}
}