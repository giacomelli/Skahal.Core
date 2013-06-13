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
		public static TEntity FindLast<TEntity, TKey>(this IRepository<TEntity, TKey> repository) 
			where TEntity : IAggregateRoot<TKey>
		{
			return repository.FindAll (repository.CountAll() - 1, 1, f => true).FirstOrDefault ();
		}

		/// <summary>
		/// Finds the last key.
		/// </summary>
		/// <returns>The last key.</returns>
		/// <param name="repository">Repository.</param>
		/// <typeparam name="TEntity">The 1st type parameter.</typeparam>
		public static long FindLastKey<TEntity, TKey>(this IRepository<TEntity, TKey> repository) 
			where TEntity : IAggregateRoot<TKey>
		{
			return 0;
		}
	}
}

