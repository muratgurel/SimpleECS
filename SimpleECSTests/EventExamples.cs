
namespace SimpleECS.Test
{
    public class EventExample
    {
        private class ExampleSystem : System
        {
            private EventQueue attackQueue = new EventQueue("attack");
            private EventQueue blockQueue = new EventQueue("block");

            public override IPredicate<Entity> entityPredicate
            {
                get
                {
                    return EntityPredicate.New;
                }
            }

            public void Update(float deltaTime)
            {
                foreach (var attack in attackQueue.GetAll())
                {
                    // Do something
                }

                foreach (var block in blockQueue.GetAll())
                {
                    // Do something else
                }

                foreach (var entity in GetEntities())
                {
                    // Now do something with your entities
                }
            }
        }
    }
}
