using System;
using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Framework.Repositories
{
    public interface IUnitOfWorkRepository
    {
		void PersistNewItem(IAggregateRoot item);
		void PersistUpdatedItem(IAggregateRoot item);
		void PersistDeletedItem(IAggregateRoot item);
    }
}