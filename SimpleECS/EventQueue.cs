using System.Collections.Generic;

namespace SimpleECS
{
    public sealed class EventQueue
    {
        private readonly Queue<object> events = new Queue<object>();

        public int pending
        {
            get
            {
                return events.Count;
            }
        }

        public EventQueue(string eventName)
        {
            // TODO: We have a leak here. EventQueue is added to a static dictionary.
            EventDispatcher.Register(eventName, this);
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
