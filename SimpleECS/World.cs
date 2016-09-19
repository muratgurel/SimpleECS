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

		public string name
		{
			get;
			private set;
		}

		public int entityCount
		{
			get
			{
				return entities.Count;
			}
		}

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

		public Entity CreateEntity()
		{
			return CreateEntity("Entity");
		}

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

	    public void AddSystem(System system)
	    {
            if (system.world != null)
	        {
	            throw new InvalidOperationException("System is already initialized in another World. You cannot use the same system instance in more than one world.");
	        }

	        systems.Add(system);
            system.OnAdd(this);
	    }

	    public void RemoveSystem(System system)
	    {
	        systems.Remove(system);
            system.OnRemove();
	    }

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
