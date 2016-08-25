using System.Collections.Generic;

namespace SimpleECS
{
	public class World
	{
		public int entityCount
		{
			get
			{
				return entities.Count;
			}
		}

		private readonly HashSet<Entity> entities = new HashSet<Entity>();

		public Entity CreateEntity()
		{
			Entity newEntity = new Entity();
			entities.Add(newEntity);
			return newEntity;
		}

		public void DestroyEntity(Entity entity)
		{
			entity.ClearAllComponents();
			entities.Remove(entity);
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

