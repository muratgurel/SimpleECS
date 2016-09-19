using System.Collections.Generic;

namespace SimpleECS
{
	/// <summary>
	/// Get a new EventQueue using the constructor
	/// new EventQueue(eventName) and register it to receive
	/// event using StartListening method. To stop receiving event,
	/// call the StopListening method.
	/// </summary>
    public sealed class EventQueue
    {
        private readonly Queue<object> events = new Queue<object>();

		public readonly string eventName;

        public int pending
        {
            get
            {
                return events.Count;
            }
        }

		public bool isRegistered
		{
			get;
			private set;
		}

        public EventQueue(string eventName)
        {
			this.eventName = eventName;
        }

		/// <summary>
		/// Registers to dispatcher. The queue will
		/// start receiving events.
		/// </summary>
		public void StartListening()
		{
			EventDispatcher.Register(eventName, this);
			isRegistered = true;
		}

		/// <summary>
		/// Unregisters from the dispatcher. The queue will
		/// not receive any events.
		/// </summary>
		public void StopListening()
		{
			EventDispatcher.Unregister(eventName, this);
			isRegistered = false;
		}

		/// <summary>
		/// Dequeue the next event. Removes the event from
		/// the queue.
		/// </summary>
        public object Get()
        {
            return events.Dequeue();
        }

		/// <summary>
		/// Dequeue all events. Queue is emptied after
		/// this.
		/// </summary>
		/// <returns>The all.</returns>
        public object[] GetAll()
        {
            object[] allEvents = events.ToArray();
            events.Clear();
            return allEvents;
        }

        internal void OnEvent(object data)
        {
            events.Enqueue(data);
        }
    }
}
