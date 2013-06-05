using System;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace Skahal.Infrastructure.Framework.Repositories
{
	/// <summary>
	/// Defines a basic interface for entities repository.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity that repository work.</typeparam>
	public interface IRepository<TEntity>  where TEntity : IAggregateRoot
	{
		#region Methods
		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The entities found.</returns>
		IEnumerable<TEntity> FindAll (Func<TEntity, bool> filter);

		/// <summary>
		/// Counts all entities.
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="filter">Filter.</param>
		long CountAll (Func<TEntity, bool> filter);

		/// <summary>
		/// Create the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		TEntity Create(TEntity entity);

		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Modify the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		void Modify(TEntity entity);
		#endregion
	}
}
