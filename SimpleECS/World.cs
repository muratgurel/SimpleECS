using System.Collections.Generic;

namespace SimpleECS
{
	public class World
	{
		private HashSet<Entity> entities = new HashSet<Entity>();

		public Entity CreateEntity()
		{
			throw new System.NotImplementedException();
		}

		public void DestroyEntity(Entity entity)
		{
			throw new System.NotImplementedException();
		}
	}
}

