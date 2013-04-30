using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Commons;
using TestSharp;

namespace Skahal.Infrastructure.Framework.UnitTests.Commons
{
	[TestFixture()]
	public class DependencyServiceTest
	{
		[Test()]
		public void Register_TypeByFunction_CreateInstance ()
		{
			DependencyService.Register<int>(() => { return 1; });
			var actual = DependencyService.Create<int>();

			Assert.AreEqual(1, actual);
		}

		[Test()]
		public void Register_TypeByInstance_CreateInstance ()
		{
			DependencyService.Register<int>(2);
			var actual = DependencyService.Create<int>();
			
			Assert.AreEqual(2, actual);
		}

		[Test()]
		public void Create_TypeNotRegistered_Exception ()
		{
			ExceptionAssert.IsThrowing(typeof(ArgumentException), () => { 
				DependencyService.Create<string>();
			});

		}
	}
}

