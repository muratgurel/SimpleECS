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
            EventDispatcher.Register(eventName, this);
        }

        public object Get()
        {
            return events.Dequeue();
        }

        public object[] GetAll()
        {
            return events.ToArray();
        }

        internal void OnEvent(object data)
        {
            events.Enqueue(data);
        }
    }
}
