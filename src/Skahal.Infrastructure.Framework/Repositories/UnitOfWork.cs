﻿#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;


#endregion

namespace Skahal.Infrastructure.Framework.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
		private Dictionary<IAggregateRoot, IUnitOfWorkRepository> m_AddedEntities;
		private Dictionary<IAggregateRoot, IUnitOfWorkRepository> m_changedEntities;
		private Dictionary<IAggregateRoot, IUnitOfWorkRepository> m_deletedEntities;
        #endregion

        #region Constructors
        public UnitOfWork()
        {
			m_AddedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
			m_changedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
			m_deletedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
        }
        #endregion

        #region Methods
		public void RegisterAdded(IAggregateRoot entity, IUnitOfWorkRepository repository)
        {
			if(!m_AddedEntities.ContainsKey(entity))
			{
           		m_AddedEntities.Add(entity, repository);
			}
        }

		public void RegisterChanged(IAggregateRoot entity, IUnitOfWorkRepository repository)
        {
			if(!m_changedEntities.ContainsKey(entity))
			{
           		m_changedEntities.Add(entity, repository);
			}
        }

		public void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository repository)
        {
			if(!m_deletedEntities.ContainsKey(entity))
			{
          		m_deletedEntities.Add(entity, repository);
			}
        }

        public void Commit()
        {
           foreach (var entity in m_deletedEntities.Keys)
            {
                m_deletedEntities[entity].PersistDeletedItem(entity);
            }

            foreach (var entity in m_AddedEntities.Keys)
            {
                m_AddedEntities[entity].PersistNewItem(entity);
            }

            foreach (var entity in m_changedEntities.Keys)
            {
                m_changedEntities[entity].PersistUpdatedItem(entity);
            }
		
			m_deletedEntities.Clear();
			m_AddedEntities.Clear();
			m_changedEntities.Clear();
        }
        #endregion
    }
}
