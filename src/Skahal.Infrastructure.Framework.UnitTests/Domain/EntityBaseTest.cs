using System;
using NUnit.Framework;
using Rhino.Mocks;
using Skahal.Infrastructure.Framework.Domain;

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
	}
}

