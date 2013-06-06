using System;
using NUnit.Framework;
using Rhino.Mocks;
using Skahal.Infrastructure.Framework.Commons;

namespace Skahal.Infrastructure.Framework.UnitTests
{
	[TestFixture()]
	public class AppServiceTest
	{
		[Test()]
		public void Started_NoListener_NoEventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);
			strategy.Raise (a => a.Started, strategy, EventArgs.Empty);
		}

		[Test()]
		public void Started_Listener_EventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);

			var raised = false;
			AppService.Started += delegate {
				raised = true;
			};
			strategy.Raise (a => a.Started, strategy, EventArgs.Empty);
			Assert.IsTrue (raised);
		}

		[Test()]
		public void BackgroundBegin_NoListener_NoEventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);
			strategy.Raise (a => a.BackgroundBegin, strategy, EventArgs.Empty);
		}

		[Test()]
		public void BackgroundBegin_Listener_EventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);

			var raised = false;
			AppService.Started += delegate {
				raised = true;
			};
			strategy.Raise (a => a.BackgroundBegin, strategy, EventArgs.Empty);
			Assert.IsTrue (raised);
		}

		[Test()]
		public void ForegroundBegin_NoListener_NoEventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);
			strategy.Raise (a => a.ForegroundBegin, strategy, EventArgs.Empty);
		}

		[Test()]
		public void ForegroundBegin_Listener_EventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);

			var raised = false;
			AppService.Started += delegate {
				raised = true;
			};
			strategy.Raise (a => a.ForegroundBegin, strategy, EventArgs.Empty);
			Assert.IsTrue (raised);
		}

		[Test()]
		public void Exited_NoListener_NoEventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);
			strategy.Raise (a => a.Exited, strategy, EventArgs.Empty);
		}

		[Test()]
		public void Exited_Listener_EventTriggered ()
		{
			var strategy = MockRepository.GenerateMock<IAppStrategy> ();
			AppService.Initialize (strategy);

			var raised = false;
			AppService.Started += delegate {
				raised = true;
			};
			strategy.Raise (a => a.Exited, strategy, EventArgs.Empty);
			Assert.IsTrue (raised);
		}
	}
}