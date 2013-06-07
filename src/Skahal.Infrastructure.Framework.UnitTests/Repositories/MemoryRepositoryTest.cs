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
			target.Add(new User() {} );
			target.Add(new User() {} );
			target.Add(new User() {} );
			target.Add(new User() {} );
			unitOfWork.Commit ();

			var actual = target.FindAll (f => f.Key == 2).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (2, actual[0].Key);
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
			target.Add(new User() { } );
			target.Add(new User() { } );
			target.Add(new User() { } );
			target.Add(new User() { } );
			unitOfWork.Commit();

			var actual = target.CountAll (f => true);
			Assert.AreEqual (4, actual);
		}

		[Test()]
		public void Add_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			target.Add (null);

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Add_EntityAlreadyExists_Exception ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			target.Add(new User() { } );
			target.Add(new User() { } );

			ExceptionAssert.IsThrowing (new InvalidOperationException("There is another entity with id '1'."), () => {
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Add_Entity_Addd ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			target.Add(new User() { } );
			var actual = target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (1, actual[0].Key);
		}

		[Test()]
		public void Delete_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			target.Remove(null);

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Delete_EntityWithIdDoesNotExists_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User() { };
			target.Remove(user);

			ExceptionAssert.IsThrowing (
				new InvalidOperationException ("There is no entity with id '1'."), 
				() => { 
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Delete_Entity_EntityDeleted ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User () { };
			target.Add (user);
			target.Remove (user);
			unitOfWork.Commit();

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (0, actual.Count);
		}

		[Test()]
		public void Modify_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			//target.Modify(null);


			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Modify_EntityWithIdDoesNotExist_Exception ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User() { };
			target[user.Key] = user;

			ExceptionAssert.IsThrowing (
			new InvalidOperationException ("There is no entity with id '1'."), 
			() => { 
				unitOfWork.Commit();
			});
		}

		[Test()]
		public void Modify_Entity_EntityModified ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User () { };
			target.Add (user);
			user = new User () {  };
			target.Add (user);

			var modifiedUser = new User() { Name = "new name" };
			target[user.Key] = modifiedUser;

			unitOfWork.Commit();

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (2, actual.Count);
			Assert.AreEqual (2, actual[1].Key);
			Assert.AreEqual ("new name", actual[1].Name);
		}
	}
}

