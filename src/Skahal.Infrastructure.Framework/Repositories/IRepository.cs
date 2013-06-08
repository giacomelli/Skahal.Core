using System;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Framework.Repositories
{
	public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        void SetUnitOfWork(IUnitOfWork unitOfWork);
		TEntity FindBy(long key);
		IEnumerable<TEntity> FindAll(Func<TEntity, bool> filter);
		IEnumerable<TEntity> FindAll(int offset, int limit, Func<TEntity, bool> filter);
		int CountAll(Func<TEntity, bool> filter);
		void Add(TEntity item);
		TEntity this[long key] { get; set; }
		void Remove(TEntity item);
    }
}
