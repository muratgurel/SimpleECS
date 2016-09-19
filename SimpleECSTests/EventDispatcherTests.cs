using NUnit.Framework;

namespace SimpleECS.Test
{
	[TestFixture]
	public class EventDispatcherTests
	{
		[Test]
		public void ShouldHandleNoListenerDispatchGracefully()
		{
			// When there were no listeners for an event,
			// it was raising an exception because the key
			// wasn't being found.
			Assert.DoesNotThrow(() =>
			{
				EventDispatcher.Dispatch("no_listener_event", null);
			});
		}

		[Test]
		public void ShouldCallListenerOnDispatch()
		{
			var newQueue = new EventQueue("anEvent");
			newQueue.StartListening();

			EventDispatcher.Dispatch("anEvent", null);
			newQueue.StopListening();

			Assert.AreEqual(1, newQueue.pending);
		}

		[Test]
		public void ShouldStopCallingListenerAfterRemove()
		{
			var newQueue = new EventQueue("anEvent");
			newQueue.StartListening();
			newQueue.StopListening();

			EventDispatcher.Dispatch("anEvent", null);

			Assert.AreEqual(0, newQueue.pending);
		}
	}
}
