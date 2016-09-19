using System.Collections.Generic;

namespace SimpleECS
{
	/// <summary>
	/// Use EventDispatcher.Dispatch(eventName, data) to dispatch
	/// event to event queues.
	/// </summary>
    public class EventDispatcher
    {
        private static readonly Dictionary<string, List<EventQueue>> queues = new Dictionary<string, List<EventQueue>>();

		/// <summary>
		/// Dispatch the specified event with an optional data. The event
		/// will be queued in EventQueues that are listening to this event.
		/// </summary>
		/// <param name="eventName">Event name</param>
		/// <param name="data">Optional data object. Can be null.</param>
        public static void Dispatch(string eventName, object data)
        {
			if (!queues.ContainsKey(eventName))
			{
				return;
			}

            foreach (var queue in queues[eventName])
            {
                queue.OnEvent(data);
            }
        }

		/// <summary>
		/// EventQueues should be registered using their StartListening method
		/// </summary>
        internal static void Register(string eventName, EventQueue queue)
        {
            if (!queues.ContainsKey(eventName))
            {
                queues.Add(eventName, new List<EventQueue>());
            }

            queues[eventName].Add(queue);
        }

		/// <summary>
		/// EventQueues should be unregistered using their StopListening method
		/// </summary>
        internal static void Unregister(string eventName, EventQueue queue)
        {
            queues[eventName].Remove(queue);
        }
    }
}
