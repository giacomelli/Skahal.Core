using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using TestSharp;
using System.Linq;
using Skahal.Infrastructure.Framework.People;
using HelperSharp;

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

			var user = new User ();
			target.Add(user);
			target.Add(new User() {} );
			target.Add(new User() {} );
			unitOfWork.Commit ();

			var actual = target.FindAll (f => f.Key == user.Key).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (user.Key, actual[0].Key);
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
		
			ExceptionAssert.IsThrowing (new ArgumentNullException("item"), () => {
				target.Add (null);
			});
		}

		[Test()]
		public void Add_EntityAlreadyAdded_IgnoresTheSecondOne ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			var user = new User (1);
			target.Add(user);
			target.Add(user);

			unitOfWork.Commit();
	
			var actual = target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (1, actual[0].Key);
		}

		[Test()]
		public void Add_Entity_Added ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);

			target.Add(new User());
			unitOfWork.Commit ();

			var actual = target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (1, actual[0].Key);
		}

		[Test()]
		public void Delete_NullEntity_ArgumentNullException ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);


			ExceptionAssert.IsThrowing (new ArgumentNullException("item"), () => {
				target.Remove(null);
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
				new InvalidOperationException ("There is no entity with id '{0}'.".With(user.Key)), 
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
			unitOfWork.Commit();

			target.Remove (user);
			unitOfWork.Commit();

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (0, actual.Count);
		}

		[Test()]
		public void Modify_EntityWithIdDoesNotExist_Added ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User() { };
			target[user.Key] = user;

			unitOfWork.Commit();

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (user.Key, actual[0].Key);
		}

		[Test()]
		public void Modify_Entity_EntityModified ()
		{
			var unitOfWork = new UnitOfWork ();
			var target = new MemoryRepository<User> (unitOfWork);
			var user = new User ();
			target.Add (user);
			user = new User ();
			target.Add (user);
			unitOfWork.Commit();

			var modifiedUser = new User(user.Key) { Name = "new name" };
			target[user.Key] = modifiedUser;

			unitOfWork.Commit();

			var actual = target.FindAll(f => true).ToList();
			Assert.AreEqual (2, actual.Count);
			Assert.AreEqual (user.Key, actual[1].Key);
			Assert.AreEqual ("new name", actual[1].Name);
		}
	}
}

