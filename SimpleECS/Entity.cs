using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class Entity
	{
		public int componentCount
		{
			get
			{
				return components.Count;
			}
		}

		private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

		public T GetComponent<T>()
		{
			throw new NotImplementedException();
		}

		public bool HasComponent<T>()
		{
			throw new NotImplementedException();
		}

		public T AddComponent<T>() where T : IComponent, new()
		{
			throw new NotImplementedException();
		}

		public void RemoveComponent<T>(T component) where T : IComponent
		{
			throw new NotImplementedException();
		}

		public void ClearAllComponents()
		{
			components.Clear();
		}
	}
}

