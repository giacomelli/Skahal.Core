using System;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Commons;
using Rhino.Mocks;

namespace Skahal.Infrastructure.Framework.UnitTests.Logging
{
	[TestFixture()]
	public class LogServiceTest
	{
		[Test()]
		public void Debug_Arguments_StragtegyDebugCalled()
		{
			var strategy = MockRepository.GenerateMock<ILogStrategy>();

			strategy.Expect(s => s.DebugWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WarningWritten += null).IgnoreArguments();
			strategy.Expect(s => s.ErrorWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WriteDebug("1", 2, 3));

			LogService.Initialize(strategy);
			LogService.Debug("1", 2, 3);

			strategy.VerifyAllExpectations();
		}

		[Test()]
		public void Warning_Arguments_StragtegyWarningCalled()
		{
			var strategy = MockRepository.GenerateStrictMock<ILogStrategy>();

			strategy.Expect(s => s.DebugWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WarningWritten += null).IgnoreArguments();
			strategy.Expect(s => s.ErrorWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WriteWarning("1", 2, 3));
			
			LogService.Initialize(strategy);
			LogService.Warning("1", 2, 3);
			
			strategy.VerifyAllExpectations();
		}

		[Test()]
		public void Error_Arguments_StragtegyErrorCalled()
		{
			var strategy = MockRepository.GenerateStrictMock<ILogStrategy>();
			
			strategy.Expect(s => s.DebugWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WarningWritten += null).IgnoreArguments();
			strategy.Expect(s => s.ErrorWritten += null).IgnoreArguments();
			strategy.Expect(s => s.WriteError("1", 2, 3));
			
			LogService.Initialize(strategy);
			LogService.Error("1", 2, 3);
			
			strategy.VerifyAllExpectations();
		}
	}
}

