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

	public static class EntityPredicateExtensions
	{
		public static EntityPredicate Include<T>(this EntityPredicate predicate)
		{
			predicate.includedTypes.Add(typeof(T));
			return predicate;
		}

		public static EntityPredicate Include<T, U>(this EntityPredicate predicate)
		{
			predicate.includedTypes.UnionWith(new Type[2] { typeof(T), typeof(U) });
			return predicate;
		}

		public static EntityPredicate Include<T, U, V>(this EntityPredicate predicate)
		{
			predicate.includedTypes.UnionWith(new Type[3] { typeof(T), typeof(U), typeof(V) });
			return predicate;
		}

		public static EntityPredicate Exclude<T>(this EntityPredicate predicate)
		{
			predicate.exludedTypes.Add(typeof(T));
			return predicate;
		}

		public static EntityPredicate Exclude<T, U>(this EntityPredicate predicate)
		{
			predicate.exludedTypes.UnionWith(new Type[2] { typeof(T), typeof(U) });
			return predicate;
		}

		public static EntityPredicate Exclude<T, U, V>(this EntityPredicate predicate)
		{
			predicate.exludedTypes.UnionWith(new Type[3] { typeof(T), typeof(U), typeof(V) });
			return predicate;
		}
	}
}

