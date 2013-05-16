using System;
using NUnit.Framework;
using Rhino.Mocks;
using Skahal.Infrastructure.Framework.Net.Messaging;

namespace Skahal.Infrastructure.Framework.UnitTests
{
	[TestFixture()]
	public class MessengerBaseTest
	{
		[Test()]
		public void Constructor_NoArguments_StartedDisconnected ()
		{
			var target = MockRepository.GenerateMock<MessengerBase>();
			Assert.AreEqual(MessengerState.Disconnected, target.State);
		}

		[Test()]
		public void SendMessage_StateDisconnected_DontSendMessage ()
		{
			bool raised = false;
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.MessageSent += delegate {
				raised = true;
			};
		
			target.SendMessage("1", "2");
			Assert.IsFalse(raised);
		}

		[Test()]
		public void SendMessage_StateConnected_SendMessage ()
		{
			bool raised = false;
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.MessageSent += delegate {
				raised = true;
			};

			target.Expect(t => t.Connect());
			target.Expect(t => t.PerformSendMessage("1", "2"));

			target.Connect();
			target.OnConnected(new ConnectedEventArgs(1));
			target.SendMessage("1", "2");
			Assert.IsTrue(raised);
		}

		[Test()]
		public void Disconnect_AlreadyDisconnected_DontDisconnectedAgain ()
		{
			bool raised = false;
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.Disconnected += delegate {
				raised = true;
			};

			target.Expect(t => t.PerformDisconnect());
			target.Disconnect();

			Assert.IsFalse(raised);

			target.VerifyAllExpectations ();
		}

		[Test()]
		public void Disconnect_Connected_Disconnected()
		{
			bool raised = false;
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.Disconnected += delegate {
				raised = true;
			};
			
			target.State = MessengerState.Connected;
			target.Expect(t => t.PerformSendMessage("__MESSENGERBASE__DISCONNECT__", "__MESSENGERBASE__QUIT__"));
			target.Expect(t => t.PerformDisconnect());
			target.Disconnect();
			Assert.IsTrue(raised);

			target.VerifyAllExpectations ();
		}

		[Test()]
		public void OnMessageReceived_NormalMessage_RaiseMessageReceiveddEvent()
		{
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.Expect(t => t.PerformSendMessage("name", "value"));
			target.Expect(t => t.OnMessageReceived(new MessageEventArgs(new Message("name", "value"))));
			target.SendMessage("name", "value");

			target.VerifyAllExpectations ();
		}

		[Test()]
		public void OnMessageReceived_DisconnectMessage_RaiseDisconnectedEvent()
		{
			var target = MockRepository.GeneratePartialMock<MessengerBase>();
			target.Expect(t => t.PerformSendMessage("__MESSENGERBASE__DISCONNECT__", "__MESSENGERBASE__QUIT__"));
			target.Expect(t => t.OnDisconnected(new DisconnectedEventArgs(DisconnectionReason.RemoteQuit)));
			target.PerformSendMessage("__MESSENGERBASE__DISCONNECT__", "__MESSENGERBASE__QUIT__");

			target.VerifyAllExpectations ();
		}
	}
}

