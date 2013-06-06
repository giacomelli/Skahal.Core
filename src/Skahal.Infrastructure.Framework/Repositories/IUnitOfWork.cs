using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace Skahal.Infrastructure.Framework.Repositories
{
    public interface IUnitOfWork
    {
		void RegisterAdded(IAggregateRoot entity, IUnitOfWorkRepository repository);
		void RegisterChanged(IAggregateRoot entity, IUnitOfWorkRepository repository);
		void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository repository);
        void Commit();
    }
}
