using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Domain.KeyGenerating;
using System.Threading.Tasks;

namespace Skahal.Infrastructure.Framework.UnitTests.Domain.KeyGenerating
{
	[TestFixture()]
	public class MemoryEntityKeyGeneratorTest
	{
		[Test()]
		public void NextKey_DiffTypes_DiffKeyValues ()
		{
			var target = new MemoryEntityKeyGenerator ();
			target.SetLastKey (typeof(int), 0);
			target.SetLastKey (typeof(string), 0);
			target.SetLastKey (typeof(double), 0);

			Assert.AreEqual(1, target.NextKey (typeof(int)));
			Assert.AreEqual(1, target.NextKey (typeof(string)));
			Assert.AreEqual(2, target.NextKey (typeof(int)));
			Assert.AreEqual(2, target.NextKey (typeof(string)));
			Assert.AreEqual(3, target.NextKey (typeof(string)));
			Assert.AreEqual(4, target.NextKey (typeof(string)));

			Assert.AreEqual(3, target.NextKey (typeof(int)));
			Assert.AreEqual(1, target.NextKey (typeof(double)));
		}

		[Test()]
		public void NextKey_Parallel_RightValues ()		{
			var target = new MemoryEntityKeyGenerator ();
			target.SetLastKey (typeof(int), 0);

			Parallel.For(0, 1000, i => {
				target.NextKey(typeof(int));
				target.NextKey(typeof(int));
			});

			Assert.AreEqual (2001, target.NextKey (typeof(int)));
		}
	}
}

