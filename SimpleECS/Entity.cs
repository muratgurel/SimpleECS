using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class Entity
	{
		/// <summary>
		/// Can be used for debug purposes. Makes it easier
		/// to distinguish entities. A default will be set
		/// by the World.
		/// </summary>
		public string name
		{
			get;
			internal set;
		}

		/// <summary>
		/// Number of components attached to this entity.
		/// </summary>
		public int componentCount
		{
			get
			{
				return components.Count;
			}
		}

		private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

		/// <summary>
		/// Return the component if present, null if not.
		/// </summary>
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

		/// <summary>
		/// Creates a new instance of the component and attaches
		/// it to the entity.
		/// </summary>
		/// <returns>The component</returns>
		public T AddComponent<T>() where T : IComponent, new()
		{
			var newComponent = new T();
			components.Add(newComponent.GetType(), newComponent);
			return newComponent;
		}

		/// <summary>
		/// Detaches the component. Make sure the component is actually
		/// present and attached to this entity.
		/// </summary>
		public void RemoveComponent<T>(T component) where T : IComponent
		{
			components.Remove(component.GetType());
		}

		/// <summary>
		/// Detach all components attached to this entity.
		/// </summary>
		public void ClearAllComponents()
		{
			components.Clear();
		}
	}
}

