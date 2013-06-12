using System;
using NUnit.Framework;
using Rhino.Mocks;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Domain.KeyGenerating;

namespace Skahal.Infrastructure.Framework.UnitTests
{
	[TestFixture()]
	public class EntityBaseTest
	{
		[Test()]
		public void EqualsOperator_NullEqualsNull_True ()
		{
			EntityBase one = null;

			Assert.IsTrue (one == null);
		}

		[Test()]
		public void Constructor_WithKeyAndConstructorWithoutKey_DiffKeys ()
		{
			var target1 = MockRepository.GenerateMock<EntityBase> (1L);
			var target2 = MockRepository.GenerateMock<EntityBase> ();

			Assert.AreNotEqual (target1.Key, target2.Key);
		}
	}
}

