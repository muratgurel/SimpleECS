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
	}
}
