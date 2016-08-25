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

		public T GetComponent<T>() where T : IComponent
		{
			IComponent component;

			if (components.TryGetValue(typeof(T), out component))
			{
				return (T)component;
			}

			return default(T);
		}

		public bool HasComponent(Type type)
		{
			return components.ContainsKey(type);
		}

		public bool HasComponent<T>() where T : IComponent
		{
			return components.ContainsKey(typeof(T));
		}

		public T AddComponent<T>() where T : IComponent, new()
		{
			T newComponent = new T();
			components.Add(newComponent.GetType(), newComponent);
			return newComponent;
		}

		public void RemoveComponent<T>(T component) where T : IComponent
		{
			components.Remove(component.GetType());
		}

		public void ClearAllComponents()
		{
			components.Clear();
		}
	}
}

