using System;
using Skahal.Infrastructure.Framework.Domain;
using System.Linq;

namespace Skahal.Infrastructure.Framework.Repositories
{
	/// <summary>
	/// Repository extensions.
	/// </summary>
	public static class RepositoryExtensions
	{
		/// <summary>
		/// Finds the last entity.
		/// </summary>
		/// <returns>The last entity.</returns>
		/// <param name="repository">Repository.</param>
		/// <typeparam name="TEntity">The 1st type parameter.</typeparam>
		public static TEntity FindLast<TEntity>(this IRepository<TEntity> repository) 
			where TEntity : IAggregateRoot
		{
			return repository.FindAll (repository.CountAll() - 1, 1, f => true).FirstOrDefault ();
		}

		/// <summary>
		/// Finds the last key.
		/// </summary>
		/// <returns>The last key.</returns>
		/// <param name="repository">Repository.</param>
		/// <typeparam name="TEntity">The 1st type parameter.</typeparam>
		public static long FindLastKey<TEntity>(this IRepository<TEntity> repository) 
			where TEntity : IAggregateRoot
		{
			return 0;
		}
	}
}

