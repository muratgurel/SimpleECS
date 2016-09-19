using System.Collections.Generic;

namespace SimpleECS
{
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

		public void StartListening()
		{
			EventDispatcher.Register(eventName, this);
			isRegistered = true;
		}

		public void StopListening()
		{
			EventDispatcher.Unregister(eventName, this);
			isRegistered = false;
		}

        public object Get()
        {
            return events.Dequeue();
        }

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
