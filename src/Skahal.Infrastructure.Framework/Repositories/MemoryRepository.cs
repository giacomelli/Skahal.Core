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
	}
}