using System;
using System.Collections.Generic;

namespace SimpleECS
{
	internal class ObjectPool<T>
	{
		private readonly Stack<T> objects = new Stack<T>();
		private readonly Func<T> objectGenerator;

		public ObjectPool(Func<T> objectGenerator)
		{
			this.objectGenerator = objectGenerator;
		}

		public ObjectPool(Func<T> objectGenerator, int initialCapacity)
		{
			this.objectGenerator = objectGenerator;

			for (var i = 0; i < initialCapacity; i++)
			{
				objects.Push(this.objectGenerator());
			}
		}

		public T GetObject()
		{
			return objects.Count > 0 ? objects.Pop() : objectGenerator();
		}

		public void PutObject(T item)
		{
			objects.Push(item);
		}
	}
}
