using System;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;
using HelperSharp;

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// A domain service base class.
	/// </summary>
	public abstract class ServiceBase<TEntity, TKey, TMainRepository, TUnitOfWork>
		where TMainRepository : IRepository<TEntity, TKey>
		where TUnitOfWork : IUnitOfWork<TKey>
		where TEntity : IAggregateRoot<TKey>
	{
		#region Constructors 
		/// <summary>
		/// Initializes a new instance of the Skahal.Infrastructure.Framework.Domain.ServiceBase/> class.
		/// </summary>
		protected ServiceBase() 
			: this(DependencyService.Create<TMainRepository>(), DependencyService.Create<TUnitOfWork>())
		{
		} 

		/// <summary>
		/// Initializes a new instance of the Skahal.Infrastructure.Framework.Domain.ServiceBase/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		protected ServiceBase(TMainRepository mainRepository, TUnitOfWork unitOfWork)
		{
			ExceptionHelper.ThrowIfNull("mainRepository", mainRepository);
			ExceptionHelper.ThrowIfNull("unitOfWork", unitOfWork);

			MainRepository = mainRepository; 
			UnitOfWork = unitOfWork;
			MainRepository.SetUnitOfWork (UnitOfWork);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the main repository.
		/// </summary>
		/// <value>The main repository.</value>
		protected TMainRepository MainRepository { get; private set; }

		/// <summary>
		/// Gets the unit of work.
		/// </summary>
		/// <value>The unit of work.</value>
		protected TUnitOfWork UnitOfWork { get; private set; }
		#endregion 

	}
}