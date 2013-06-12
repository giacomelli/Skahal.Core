using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Domain.KeyGenerating;

namespace Skahal.Infrastructure.Framework.UnitTests.Repositories
{
	[TestFixture()]
	public class RepositoryExtensionsTest
	{
		#region Fields
		private MemoryRepository<User> m_userRepository;
		private IUnitOfWork m_unitOfWork = new MemoryUnitOfWork();
		#endregion

		[SetUp]
		public void InitializeFixture()
		{
			m_userRepository = new MemoryRepository<User> (m_unitOfWork);
			var keyGenerator = new MemoryLongEntityKeyGenerator ();
			keyGenerator.SetLastKey (typeof(User), 0);
			EntityKeyGenerator.Initialize (keyGenerator);
		}

		[Test()]
		public void FindLast_NoEntities_Null ()
		{
			Assert.IsNull (m_userRepository.FindLast());
		}

		[Test()]
		public void FindLast_OnlyOneEntity_TheOneEntity ()
		{
			var entity = new User() { Name = "1" };
			m_userRepository.Add (entity);
			m_unitOfWork.Commit();

			Assert.AreEqual ("1", m_userRepository.FindLast().Name);
		}

		[Test()]
		public void FindLast_ManyEntities_LastOne ()
		{
			var entity = new User () { Name = "1" };
			m_userRepository.Add (entity);

			entity = new User() { Name = "2" };
			m_userRepository.Add (entity);

			entity = new User() { Name = "3" };
			m_userRepository.Add (entity);

			m_unitOfWork.Commit();

			Assert.AreEqual ("3", m_userRepository.FindLast().Name);
		}
	}
}