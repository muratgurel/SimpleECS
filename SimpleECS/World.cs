using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class World
	{
		public delegate void EntityEventHandler(World world, Entity entity);

		/// <summary>
		/// The entity is ready to be used when this event is invoked.
		/// You can perform operations on the entity inside the handler.
		/// </summary>
		public event EntityEventHandler OnEntityCreate;
		/// <summary>
		/// It is called before all components are removed from the entity,
		/// and before it's put in the pool again. So you can query the
		/// entity inside the handler.
		/// </summary>
		public event EntityEventHandler OnEntityDestroy;

		/// <summary>
		/// Can be used for debug purposes. A default will
		/// be set.
		/// </summary>
		public string name
		{
			get;
			private set;
		}

		/// <summary>
		/// Number of entities that are active inside the world.
		/// The ones in the pool are not counted towards this value.
		/// </summary>
		public int entityCount
		{
			get
			{
				return entities.Count;
			}
		}

		/// <summary>
		/// Number of systems attached to this world.
		/// </summary>
	    public int systemCount
	    {
	        get
	        {
	            return systems.Count;
	        }
	    }

        private readonly HashSet<System> systems = new HashSet<System>();
        private readonly HashSet<Entity> entities = new HashSet<Entity>();
        private readonly ObjectPool<Entity> pool = new ObjectPool<Entity>(() => new Entity());

		public World()
		{
			name = "World";
		}

		public World(string name)
		{
			this.name = name;
		}

		/// <summary>
		/// Creates an entity with default name. The
		/// pool will be first used if any entity can be
		/// reused.
		/// </summary>
		public Entity CreateEntity()
		{
			return CreateEntity("Entity");
		}

		/// <summary>
		/// Creates an entity with a custom name. The
		/// pool will be first used if any entity can be
		/// reused.
		/// </summary>
		public Entity CreateEntity(string name)
		{
			Entity newEntity = pool.GetObject();
			newEntity.name = name;

			entities.Add(newEntity);

			if (OnEntityCreate != null)
			{
				OnEntityCreate(this, newEntity);
			}

			return newEntity;
		}

		/// <summary>
		/// You should Destroy the entities when you are
		/// done with them. It removes all components and
		/// puts the entity back to the pool for reuse.
		/// </summary>
		public void DestroyEntity(Entity entity)
		{
			if (OnEntityDestroy != null)
			{
				OnEntityDestroy(this, entity);
			}

			entity.ClearAllComponents();
			entities.Remove(entity);
			pool.PutObject(entity);
		}

		/// <summary>
		/// Add a system to the world. The system is initialized
		/// when added.
		/// </summary>
	    public void AddSystem(System system)
	    {
            if (system.world != null)
	        {
	            throw new InvalidOperationException("System is already initialized in another World. You cannot use the same system instance in more than one world.");
	        }

	        systems.Add(system);
            system.OnAdd(this);
	    }

		/// <summary>
		/// Remove the system from the world. The system is
		/// deinitialized when removed.
		/// </summary>
	    public void RemoveSystem(System system)
	    {
	        systems.Remove(system);
            system.OnRemove();
	    }

		/// <summary>
		/// Use this method to query the world and get entities that
		/// match the predicate. You can use EntityPredicate class
		/// or roll your own predicates that implement IPredicate<Entity> interface.
		/// </summary>
		/// <returns>The entities that match the predicate.</returns>
		/// <param name="predicate">Predicate.</param>
		public List<Entity> GetEntities(IPredicate<Entity> predicate)
		{
			var filteredEntities = new List<Entity>();

			foreach (var entity in entities)
			{
				if (predicate.Matches(entity))
				{
					filteredEntities.Add(entity);
				}
			}

			return filteredEntities;
		}
	}
}
