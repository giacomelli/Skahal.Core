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
	public class MemoryRepository<TEntity> : RepositoryBase<TEntity> where TEntity : IAggregateRoot
	{
		#region Fields
		private List<TEntity> m_entities = new List<TEntity> ();
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Repositories.MemoryRepository`1"/> class.
		/// </summary>
		public MemoryRepository()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Repositories.MemoryRepository`1"/> class.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		public MemoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
		#endregion

		#region implemented abstract members of RepositoryBase

		public override TEntity FindBy (long id)
		{
			return FindAll (e => e.Key == id).FirstOrDefault();
		}

		public override IEnumerable<TEntity> FindAll (int offset, int limit, Func<TEntity, bool> filter)
		{
			ExceptionHelper.ThrowIfNull ("filter", filter);

			return m_entities
				.Where (e => filter(e))
				.OrderBy(e => e.Key)
				.Skip(offset).Take(limit);
		}

		public override int CountAll (Func<TEntity, bool> filter)
		{
			ExceptionHelper.ThrowIfNull ("filter", filter);
			return m_entities.Count (e => filter(e));
		}

		protected override void PersistNewItem (TEntity item)
		{
			ExceptionHelper.ThrowIfNull ("item", item);

			if (m_entities.FirstOrDefault (e => e.Key == item.Key) != null) {
				throw new InvalidOperationException ("There is another entity with id '{0}'.".With(item.Key));
			}

			m_entities.Add (item);
		}

		protected override void PersistUpdatedItem (TEntity item)
		{
			ExceptionHelper.ThrowIfNull ("item", item);
			
			PersistDeletedItem (item);
			PersistNewItem (item);
		}

		protected override void PersistDeletedItem (TEntity item)
		{
			ExceptionHelper.ThrowIfNull ("item", item);

			var old = m_entities.FirstOrDefault (e => e.Key == item.Key);

			if (old == null) {
				throw new InvalidOperationException ("There is no entity with id '{0}'.".With(item.Key));
			}

			m_entities.Remove (old);
		}

		#endregion



//		#region IRepository implementation
//		/// <summary>
//		/// Finds all entities that matches the filter.
//		/// </summary>
//		/// <returns>The entities found.</returns>
//		/// <param name="filter">Filter.</param>
//		public IEnumerable<TEntity> FindAll (Func<TEntity, bool> filter)
//		{
//			ExceptionHelper.ThrowIfNull ("filter", filter);
//
//			return m_entities.Where (e => filter(e)).OrderBy(e => e.Id);
//		}
//
//		/// <summary>
//		/// Counts all entities.
//		/// </summary>
//		/// <returns>The all.</returns>
//		/// <param name="filter">Filter.</param>
//		public long CountAll (Func<TEntity, bool> filter)
//		{
//			ExceptionHelper.ThrowIfNull ("filter", filter);
//			return m_entities.Count (e => filter(e));
//		}
//
//
//		/// <summary>
//		/// Add the specified entity.
//		/// </summary>
//		/// <param name="entity">Entity.</param>
//		public TEntity Add (TEntity entity)
//		{
//			ExceptionHelper.ThrowIfNull ("entity", entity);
//
//			if (m_entities.FirstOrDefault (e => e.Id == entity.Id) != null) {
//				throw new InvalidOperationException ("There is another entity with id '{0}'.".With(entity.Id));
//			}
//
//			if (entity.Id == 0) {
//				entity.Id = m_nextId++;
//			}
//
//			m_entities.Add (entity);
//
//			return entity;
//		}
//
//		/// <summary>
//		/// Delete the specified entity.
//		/// </summary>
//		/// <param name="entity">Entity.</param>
//		public void Delete (TEntity entity)
//		{
//			ExceptionHelper.ThrowIfNull ("entity", entity);
//
//			var old = m_entities.FirstOrDefault (e => e.Id == entity.Id);
//
//			if (old == null) {
//				throw new InvalidOperationException ("There is no entity with id '{0}'.".With(entity.Id));
//			}
//
//			m_entities.Remove (old);
//		}
//
//		/// <summary>
//		/// Modify the specified entity.
//		/// </summary>
//		/// <param name="entity">Entity.</param>
//		public void Modify (TEntity entity)
//		{
//			ExceptionHelper.ThrowIfNull ("entity", entity);
//	
//			Delete (entity);
//			Add (entity);
//		}
//
//		#endregion
	}
}