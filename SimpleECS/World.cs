using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class World
	{
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
			Entity newEntity = pool.GetObject();
			entities.Add(newEntity);
			return newEntity;
		}

		public void DestroyEntity(Entity entity)
		{
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
            system.Initialize(this);
	    }

	    public void RemoveSystem(System system)
	    {
	        systems.Remove(system);
            system.Uninitialize();
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
