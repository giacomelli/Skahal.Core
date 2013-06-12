using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using System;

namespace Skahal.Infrastructure.Framework.Repositories
{
	/// <summary>
	/// Defines the interface of a repository entity.
	/// </summary>
	public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
		/// <summary>
		/// Sets the unit of work.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
        void SetUnitOfWork(IUnitOfWork unitOfWork);

		/// <summary>
		/// Finds the entity by the key.
		/// </summary>
		/// <returns>The found entity.</returns>
		/// <param name="key">Key.</param>
		TEntity FindBy(long key);

		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="filter">Filter.</param>
		IEnumerable<TEntity> FindAll(Func<TEntity, bool> filter);

		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="offset">Offset.</param>
		/// <param name="limit">Limit.</param>
		/// <param name="filter">Filter.</param>
		IEnumerable<TEntity> FindAll(int offset, int limit, Func<TEntity, bool> filter);

		/// <summary>
		/// Finds all entities.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="offset">Offset.</param>
		/// <param name="limit">Limit.</param>
		IEnumerable<TEntity> FindAll(int offset, int limit);

		/// <summary>
		/// Counts all entities that matches the filter.
		/// </summary>
		/// <returns>The number of the entities that matches the filter.</returns>
		/// <param name="filter">Filter.</param>
		int CountAll(Func<TEntity, bool> filter);

		/// <summary>
		/// Counts all entities
		/// </summary>
		/// <returns>The number of the entities that matches the filter.</returns>
		int CountAll();

		/// <summary>
		/// Add the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		void Add(TEntity item);

		/// <summary>
		/// Gets or sets the <see cref="Skahal.Infrastructure.Framework.Repositories.IRepository&lt;TEntity&gt;"/> with the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		TEntity this[long key] { get; set; }

		/// <summary>
		/// Remove the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		void Remove(TEntity item);
    }
}
