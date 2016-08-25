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

		public T AddComponent<T>() where T : IComponent, new()
		{
			throw new NotImplementedException();
		}

		public void RemoveComponent<T>(T component) where T : IComponent
		{
			throw new NotImplementedException();
		}

		internal void ClearAllComponents()
		{
			components.Clear();
		}
	}
}

