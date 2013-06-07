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
		public void Register_TypeByFunction_AddInstance ()
		{
			DependencyService.Register<int>(() => { return 1; });
			var actual = DependencyService.Add<int>();

			Assert.AreEqual(1, actual);
		}

		[Test()]
		public void Register_TypeByFunctionWithArgument_AddInstance ()
		{
			DependencyService.Register<string>((chars) => { return new String((char[])chars); });
			var actual = DependencyService.Add<string>(new char[] { 't', 'e', 's', 't', 'e' });

			Assert.AreEqual("teste", actual);
		}

		[Test()]
		public void Register_TypeByInstance_AddInstance ()
		{
			DependencyService.Register<int>(2);
			var actual = DependencyService.Add<int>();
			
			Assert.AreEqual(2, actual);
		}

		[Test()]
		public void Add_TypeNotRegistered_Exception ()
		{
			ExceptionAssert.IsThrowing(typeof(ArgumentException), () => { 
				DependencyService.Add<string>();
			});

		}
	}
}

