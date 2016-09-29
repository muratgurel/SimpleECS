using System;
using System.Collections.Generic;

namespace SimpleECS
{
	/// <summary>
	/// All systems should implement this class, and added to a world.
	/// It's your responsibility to add an Update method and invoke it
	/// yourself. A system only provides you the world & entity fetching
	/// mechanizm along side an initialize & uninitialize methods.
	/// </summary>
    public abstract class System
    {
		/// <summary>
		/// The world that this system is working on. You have add this system
		/// to a world using World.AddSystem(system) for this value to be set.
		/// Otherwise, it is null. You can add a system to a world only once.
		/// You have to remove it from its world to add it to another world.
		/// GetEntities method uses this world to fetch entities.
		/// </summary>
        public World world
        {
            get;
            private set;
        }

		/// <summary>
		/// Override this method to do your initialization. For example
		/// event queue subscriptions, other events & initing your
		/// predicates. It is invoked after you add the system to a world.
		/// Inside this method, "world" reference is already set.
		/// </summary>
		protected virtual void Initialize() { }
		/// <summary>
		/// Override this method to undo everything you do in your Initialize
		/// implementation & do clean-up. "world" reference is still set inside
		/// this method.
		/// </summary>
		protected virtual void Uninitialize() { }

		/// <summary>
		/// Pass in a predicate to query & fetch entities from the world
		/// the system is added to. You can also do it manually by calling
		/// GetEntity method of World. This is a convenience method.
		/// </summary>
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
