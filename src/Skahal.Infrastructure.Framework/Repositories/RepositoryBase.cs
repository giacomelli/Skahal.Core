//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2011 Skahal Studios
using System;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Framework.Repositories
{
	public abstract class RepositoryBase<TEntity>
		: IRepository<TEntity>, IUnitOfWorkRepository where TEntity : IAggregateRoot
    {
        #region Private Fields

        private IUnitOfWork m_unitOfWork;

        #endregion

        #region Constructors

        protected RepositoryBase() 
            : this(null)
        {
        }

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            m_unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository<T> Members

        public abstract TEntity FindBy(long id);

		public virtual IEnumerable<TEntity> FindAll(Func<TEntity, bool> filter)
		{
			return FindAll(1, int.MaxValue, filter);
		}
		
		public abstract IEnumerable<TEntity> FindAll(int offset, int limit, Func<TEntity, bool> filter);
		
		public abstract int CountAll(Func<TEntity, bool> filter);

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            m_unitOfWork = unitOfWork;
        }

        public void Add(TEntity item)
        {
            if (m_unitOfWork != null)
            {
                m_unitOfWork.RegisterAdded(item, this);
            }
        }

        public void Remove(TEntity item)
        {
            if (m_unitOfWork != null)
            {
                m_unitOfWork.RegisterRemoved(item, this);
            }
        }

        public TEntity this[long key]
        {
            get
            {
                return FindBy(key);
            }
            set
            {
                if (FindBy(key) == null)
                {
                    Add(value);
                }
                else
                {
                    if (m_unitOfWork != null)
                    {
                        m_unitOfWork.RegisterChanged(value, this);
                    }
                }
            }
        }

        #endregion

        #region IUnitOfWorkRepository Members

		public virtual void PersistNewItem(IAggregateRoot item)
        {
            PersistNewItem((TEntity)item);
        }

		public virtual void PersistUpdatedItem(IAggregateRoot item)
        {
            PersistUpdatedItem((TEntity)item);
        }

		public virtual void PersistDeletedItem(IAggregateRoot item)
        {
            PersistDeletedItem((TEntity)item);
        }

        #endregion

        #region Properties

        protected IUnitOfWork UnitOfWork
        {
            get { return m_unitOfWork; }
        }

        #endregion

        #region Methods

        protected abstract void PersistNewItem(TEntity item);
        protected abstract void PersistUpdatedItem(TEntity item);
        protected abstract void PersistDeletedItem(TEntity item);

        #endregion
    }
}