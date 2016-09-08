using System;
using System.Collections.Generic;

namespace SimpleECS
{
    public abstract class System
    {
        public World world
        {
            get;
            private set;
        }

		protected virtual void Initialize() { }
		protected virtual void Uninitialize() { }

		public List<Entity> GetEntities(IPredicate<Entity> predicate)
        {
            if (world == null)
            {
                throw new ArgumentNullException("world", "System is not added to any world. Use World.AddSystem()");
            }

			return world.GetEntities(predicate);
        }

        internal void OnAdd(World world)
        {
            this.world = world;
			Initialize();
        }

        internal void OnRemove()
        {
			Uninitialize();
            world = null;
        }
    }
}
