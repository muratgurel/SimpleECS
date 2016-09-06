using System;
using System.Collections.Generic;

namespace SimpleECS
{
    public abstract class System
    {
        public abstract IPredicate<Entity> entityPredicate { get; }

        public World world
        {
            get;
            private set;
        }

        public List<Entity> GetEntities()
        {
            if (world == null)
            {
                throw new ArgumentNullException("world", "System is not added to any world. Use World.AddSystem()");
            }

            return world.GetEntities(entityPredicate);
        }

        internal void Initialize(World world)
        {
            this.world = world;
        }

        internal void Uninitialize()
        {
            world = null;
        }
    }
}
