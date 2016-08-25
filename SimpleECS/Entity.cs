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

		private Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();
	}
}

