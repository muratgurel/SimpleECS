using System.Collections.Generic;

namespace SimpleECS
{
    public class EventDispatcher
    {
        private static readonly Dictionary<string, List<EventQueue>> queues = new Dictionary<string, List<EventQueue>>();

        public static void Dispatch(string eventName, object data)
        {
            foreach (var queue in queues[eventName])
            {
                queue.OnEvent(data);
            }
        }

        internal static void Register(string eventName, EventQueue queue)
        {
            if (!queues.ContainsKey(eventName))
            {
                queues.Add(eventName, new List<EventQueue>());
            }

            queues[eventName].Add(queue);
        }

        internal static void Unregister(string eventName, EventQueue queue)
        {
            queues[eventName].Remove(queue);
        }
    }
}
