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
	}
}

