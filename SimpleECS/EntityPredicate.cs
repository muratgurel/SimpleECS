using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class EntityPredicate : IPredicate<Entity>
	{
		internal readonly HashSet<Type> includedTypes = new HashSet<Type>();
		internal readonly HashSet<Type> exludedTypes = new HashSet<Type>();

		public static EntityPredicate New
		{
			get
			{
				return new EntityPredicate();
			}
		}

		public EntityPredicate Include<T>()
		{
			includedTypes.Add(typeof(T));
			return this;
		}

		public EntityPredicate Exclude<T>()
		{
			exludedTypes.Add(typeof(T));
			return this;
		}

		public bool Matches(Entity obj)
		{
			foreach (var type in includedTypes)
			{
				if (!obj.HasComponent(type))
				{
					return false;
				}
			}

			foreach (var type in exludedTypes)
			{
				if (obj.HasComponent(type))
				{
					return false;
				}
			}

			return true;
		}
	}
}

