using NUnit.Framework;

namespace SimpleECS.Test
{
	[TestFixture]
	public class EventQueueTests
	{
		private EventQueue eventQueue;

		[SetUp]
		public void SetUp()
		{
			eventQueue = new EventQueue("an_event");
		}

		[Test]
		public void ShouldRegisterItselfOnStartListening()
		{
			eventQueue.StartListening();

			EventDispatcher.Dispatch("an_event", null);
			Assert.AreEqual(1, eventQueue.pending);

			eventQueue.StopListening();
		}

		[Test]
		public void ShouldUnregisterItselfOnStopListening()
		{
			eventQueue.StartListening();
			eventQueue.StopListening();

			EventDispatcher.Dispatch("an_event", null);
			Assert.AreEqual(0, eventQueue.pending);
		}

		[Test]
		public void GetShouldDequeueFirstEventInPending()
		{
			eventQueue.StartListening();

			EventDispatcher.Dispatch("an_event", "first");
			EventDispatcher.Dispatch("an_event", "second");

			Assert.AreEqual(2, eventQueue.pending);
			Assert.AreEqual("first", eventQueue.Get());
			Assert.AreEqual(1, eventQueue.pending);
			Assert.AreEqual("second", eventQueue.Get());
			Assert.AreEqual(0, eventQueue.pending);

			eventQueue.StopListening();
		}

		[Test]
		public void GetAllShouldDequeueAllEventsInQueue()
		{
			eventQueue.StartListening();

			EventDispatcher.Dispatch("an_event", null);
			EventDispatcher.Dispatch("an_event", null);

			Assert.AreEqual(2, eventQueue.pending);
			Assert.AreEqual(2, eventQueue.GetAll().Length);
			Assert.AreEqual(0, eventQueue.pending);

			eventQueue.StopListening();
		}

		[Test]
		public void ShouldEnqueueEventOnReceive()
		{
			eventQueue.StartListening();

			Assert.AreEqual(0, eventQueue.pending);
			EventDispatcher.Dispatch("an_event", null);
			EventDispatcher.Dispatch("an_event", null);
			Assert.AreEqual(2, eventQueue.pending);

			eventQueue.StopListening();
		}

		[Test]
		public void ShouldUpdateIsRegistered()
		{
			eventQueue.StartListening();
			Assert.IsTrue(eventQueue.isRegistered);

			eventQueue.StopListening();
			Assert.IsFalse(eventQueue.isRegistered);
		}
	}
}
