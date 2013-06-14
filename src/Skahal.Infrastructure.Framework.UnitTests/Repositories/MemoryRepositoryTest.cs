using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using TestSharp;
using System.Linq;
using Skahal.Infrastructure.Framework.People;
using HelperSharp;

namespace Skahal.Infrastructure.Framework.UnitTests.Repositories
{
	[TestFixture()]
	public class MemoryRepositoryTest
	{
		#region Fields
		private MemoryUnitOfWork<string> m_unitOfWork;
		private MemoryRepository<User, string> m_target;
		#endregion

		#region Initialize
		[SetUp]
		public void InitializeTest()
		{
			m_unitOfWork = new MemoryUnitOfWork<string> ();
			m_target = new MemoryRepository<User, string> (m_unitOfWork, (u) => { return Guid.NewGuid().ToString(); });
		}
		#endregion

		#region Tests
		[Test()]
		public void FindAll_NullFilter_ArgumentNullException ()
		{
			ExceptionAssert.IsThrowing (new ArgumentNullException("filter"), () => {
				m_target.FindAll (null);
			});
		}

		[Test()]
		public void FindAll_Filter_EntitiesFiltered ()
		{
			m_target.Add(new User() {} );

			var user = new User ();
			m_target.Add(user);
			m_target.Add(new User() {} );
			m_target.Add(new User() {} );
			m_unitOfWork.Commit ();

			var actual = m_target.FindAll (f => f.Key == user.Key).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (user.Key, actual[0].Key);
		}

		
		[Test()]
		public void CountAll_NullFilter_ArgumentNullException ()
		{
			ExceptionAssert.IsThrowing (new ArgumentNullException("filter"), () => {
				m_target.CountAll (null);
			});
		}

		[Test()]
		public void CountAll_Filter_EntitiesFiltered ()
		{
			m_target.Add(new User() { } );
			m_target.Add(new User() { } );
			m_target.Add(new User() { } );
			m_target.Add(new User() { } );
			m_unitOfWork.Commit();

			var actual = m_target.CountAll (f => true);
			Assert.AreEqual (4, actual);
		}

		[Test()]
		public void Add_NullEntity_ArgumentNullException ()
		{
			ExceptionAssert.IsThrowing (new ArgumentNullException("item"), () => {
				m_target.Add (null);
			});
		}

		[Test()]
		public void Add_EntityAlreadyAdded_Exception()
		{
			var user = new User ("1");
			m_target.Add(user);
			m_target.Add(user);

			ExceptionAssert.IsThrowing (new InvalidOperationException ("There is another entity with id '1'."), () => {
				m_unitOfWork.Commit ();
			});
	
			var actual = m_target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual ("1", actual[0].Key);
		}

		[Test()]
		public void Add_Entity_Added ()
		{
			m_target.Add(new User());
			m_unitOfWork.Commit ();

			var actual = m_target.FindAll(f => true).ToList();

			Assert.AreEqual (1, actual.Count);
			Assert.IsFalse (String.IsNullOrWhiteSpace(actual[0].Key));
		}

		[Test()]
		public void Delete_NullEntity_ArgumentNullException ()
		{
			ExceptionAssert.IsThrowing (new ArgumentNullException("item"), () => {
				m_target.Remove(null);
			});
		}

		[Test()]
		public void Delete_EntityWithIdDoesNotExists_ArgumentNullException ()
		{
			var user = new User() { };
			m_target.Remove(user);

			ExceptionAssert.IsThrowing (
				new InvalidOperationException ("There is no entity with id '{0}'.".With(user.Key)), 
				() => { 
				m_unitOfWork.Commit();
			});
		}

		[Test()]
		public void Delete_Entity_EntityDeleted ()
		{
			var user = new User () { };
			m_target.Add (user);
			m_unitOfWork.Commit();

			m_target.Remove (user);
			m_unitOfWork.Commit();

			var actual = m_target.FindAll(f => true).ToList();
			Assert.AreEqual (0, actual.Count);
		}

		[Test()]
		public void Modify_EntityWithIdDoesNotExist_Added ()
		{
			var user = new User();
			Assert.IsNull(user.Key);

			m_target[user.Key] = user;

			m_unitOfWork.Commit();

			Assert.IsNotNull(user.Key);

			var actual = m_target.FindAll(f => true).ToList();
			Assert.AreEqual (1, actual.Count);
			Assert.AreEqual (user.Key, actual[0].Key);
		}

		[Test()]
		public void Modify_Entity_EntityModified ()
		{
			var user = new User ();
			m_target.Add (user);
			user = new User ();
			m_target.Add (user);
			m_unitOfWork.Commit();

			var modifiedUser = new User(user.Key) { Name = "new name" };
			m_target[user.Key] = modifiedUser;

			m_unitOfWork.Commit();

			var actual = m_target.FindAll (f => f == user).FirstOrDefault ();
			Assert.AreEqual (user.Key, actual.Key);
			Assert.AreEqual ("new name", actual.Name);
		}
		#endregion
	}
}

