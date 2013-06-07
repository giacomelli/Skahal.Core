using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using TestSharp;
using System.Linq;
using Skahal.Infrastructure.Framework.People;

namespace Skahal.Infrastructure.Framework.UnitTests
{
	[TestFixture()]
	public class MemoryRepositoryTest
	{
		[Test()]
		public void FindAll_NullFilter_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			ExceptionAssert.IsThrowing (new ArgumentNullException("filter"), () => {
				target.FindAll (null);
			});
		}

		[Test()]
		public void FindAll_Filter_EntitiesFiltered ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			target.Add(new User());
			target.Add(new User());
			target.Add(new User());
			target.Add(new User());

			var actual = target.FindAll (f => f.Id == 2).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (2, actual[0].Id);
		}

		
		[Test()]
		public void CountAll_NullFilter_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			ExceptionAssert.IsThrowing (new ArgumentNullException("filter"), () => {
				target.CountAll (null);
			});
		}

		[Test()]
		public void CountAll_Filter_EntitiesFiltered ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			target.Add(new User());
			target.Add(new User());
			target.Add(new User());
			target.Add(new User());

			var actual = target.CountAll (f => true);
			Assert.AreEqual (4, actual);
		}

		[Test()]
		public void Add_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				target.Add (null);
			});
		}

		[Test()]
		public void Add_EntityAlreadyExists_Exception ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			target.Add(new User());

			ExceptionAssert.IsThrowing (new InvalidOperationException("There is another entity with id '1'."), () => {
				target.Add(new User());
			});
		}

		[Test()]
		public void Add_Entity_Addd ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			target.Add(new User() { Id = 0 } );
			var actual = target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (1, actual[0].Id);
		}

		[Test()]
		public void Delete_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				target.Remove(null);
			});
		}

		[Test()]
		public void Delete_EntityWithIdDoesNotExists_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User() { Id = 1 };

			ExceptionAssert.IsThrowing (
				new InvalidOperationException ("There is no entity with id '1'."), 
				() => { 
				target.Remove(user);
			});
		}

		[Test()]
		public void Delete_Entity_EntityDeleted ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User () { Id = 1 };
			target.Add (user);
			target.Remove (user);

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (0, actual.Count);
		}

		[Test()]
		public void Modify_EntityWithIdDoesNotExist_Exception ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User();

			ExceptionAssert.IsThrowing (
			new InvalidOperationException ("There is no entity with id '1'."), 
			() => { 
				target[user.Id] = user;
			});
		}

		[Test()]
		public void Modify_Entity_EntityModified ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User () { Id = 1 };
			target.Add (user);
			user = new User () { Id = 2 };
			target.Add (user);

			var modifiedUser = new User() { Id = 2, Name = "new name" };
			target[modifiedUser.Id] = modifiedUser;

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (2, actual.Count);
			Assert.AreEqual (2, actual[1].Id);
			Assert.AreEqual ("new name", actual[1].Name);
		}
	}
}

