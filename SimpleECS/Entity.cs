using System;
using System.Collections.Generic;

namespace SimpleECS
{
	public class Entity
	{
		public Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();
	}
}

