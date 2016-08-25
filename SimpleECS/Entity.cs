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
	}
}

