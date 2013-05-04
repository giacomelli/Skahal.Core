using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;
using Skahal.Infrastructure.Framework.Logging;
using ProtoBuf.Meta;

namespace Skahal.Infrastructure.Unity.Repositories
{
	/// <summary>
	/// Protobuf repository base.
	/// </summary>
	public abstract class ProtobufRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		#region Fields
		private string m_repositoryFolder;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Unity.Repositories.ProtobufRepositoryBase`1"/> class.
		/// </summary>
		protected ProtobufRepositoryBase()
		{
			var entityType = typeof(TEntity);

			m_repositoryFolder = Path.Combine(Application.persistentDataPath, entityType.FullName);
		
			if(!Directory.Exists(m_repositoryFolder))
			{
				Directory.CreateDirectory(m_repositoryFolder);
			}

			LogService.Debug("{0}: using folder '{1}' as data folder.", GetType().Name, m_repositoryFolder);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Get all entities.
		/// </summary>
		public virtual IQueryable<TEntity> All ()
		{
			var allIds = GetAllIds ();
			var result = new List<TEntity> ();
			
			foreach (var id in allIds) {
				using(var stream = File.OpenRead (GetFileName(id)))
				{
					var r = ProtoBuf.Serializer.Deserialize<TEntity>(stream);
					result.Add(r);
				}
			}
			
			return result.AsQueryable();
		}
		
		/// <summary>
		/// Create the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual TEntity Create (TEntity entity)
		{
			entity.Id = GetLastId () + 1;
			
			Modify(entity);

			return entity;
		}
		
		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual void Delete(TEntity entity)
		{
			File.Delete(GetFileName(entity.Id));
		}
		
		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public virtual void Delete (int id)
		{ 
			File.Delete(GetFileName (id));
		} 
		
		/// <summary>
		/// Modify the specified entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		public virtual void Modify (TEntity entity)
		{
			using(var stream = File.OpenWrite (GetFileName(entity.Id)))
			{
				ProtoBuf.Serializer.Serialize(stream, entity);
			}
		}
		#endregion	
		
		#region Fields
		private string GetFileName (long id)
		{
			return Path.Combine(m_repositoryFolder, id + ".bin");
		}
		
		private long GetLastId ()
		{
			return GetAllIds().LastOrDefault();
		}

		private long[] GetAllIds()
		{
			var files = Directory.GetFiles(m_repositoryFolder, "*.bin");
			LogService.Debug("{0}: {1} files found on data folder '{2}'.", GetType().Name, files.Length, m_repositoryFolder);

			return files.Select(f => long.Parse(Path.GetFileNameWithoutExtension(f))).ToArray();
		}
		#endregion
	}
}

