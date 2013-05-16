using System;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;

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
		/// Find an entity by the filter.
		/// </summary>
		/// <param name="filter">Filter.</param>
		TEntity Find(Func<TEntity, bool> filter);

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
		/// Delete the entity with the specified id.
		/// </summary>
		/// <param name="id">Identifier.</param>
		void Delete(int id);

		/// <summary>
		/// Modify the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		void Modify(TEntity entity);
		#endregion
	}
}
