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
			var target = new MemoryRepository<User> ();

			target.Add(new User() { Key = 1 } );

			ExceptionAssert.IsThrowing (new InvalidOperationException("There is another entity with id '1'."), () => {
				target.Add(new User() { Key = 1 } );
			});
		}

		[Test()]
		public void Add_Entity_Addd ()
		{
			var target = new MemoryRepository<User> ();

			target.Add(new User() { Key = 0 } );
			var actual = target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (1, actual[0].Key);
		}

		[Test()]
		public void Delete_NullEntity_ArgumentNullException ()
		{
			var target = new MemoryRepository<User> ();

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				target.Delete(null);
			});
		}

		[Test()]
		public void Delete_EntityWithIdDoesNotExists_ArgumentNullException ()
		{
			var target = new MemoryRepository<User> ();
			var user = new User() { Key = 1 };

			ExceptionAssert.IsThrowing (
				new InvalidOperationException ("There is no entity with id '1'."), 
				() => { 
				target.Delete(user);
			});
		}

		[Test()]
		public void Delete_Entity_EntityDeleted ()
		{
			var target = new MemoryRepository<User> ();
			var user = new User () { Key = 1 };
			target.Add (user);
			target.Delete (user);

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (0, actual.Count);
		}

		[Test()]
		public void Modify_NullEntity_ArgumentNullException ()
		{
			var target = new MemoryRepository<User> ();

			ExceptionAssert.IsThrowing (new ArgumentNullException("entity"), () => {
				target.Modify(null);
			});
		}

		[Test()]
		public void Modify_EntityWithIdDoesNotExist_Exception ()
		{
			var target = new MemoryRepository<User> ();
			var user = new User() { Key = 1 };

			ExceptionAssert.IsThrowing (
			new InvalidOperationException ("There is no entity with id '1'."), 
			() => { 
				target.Modify(user);
			});
		}

		[Test()]
		public void Modify_Entity_EntityModified ()
		{
			var target = new MemoryRepository<User> ();
			var user = new User () { Key = 1 };
			target.Add (user);
			user = new User () { Key = 2 };
			target.Add (user);

			var modifiedUser = new User() { Key = 2, Name = "new name" };
			target.Modify (modifiedUser);

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (2, actual.Count);
			Assert.AreEqual (2, actual[1].Key);
			Assert.AreEqual ("new name", actual[1].Name);
		}
	}
}

