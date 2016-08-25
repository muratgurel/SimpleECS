
namespace SimpleECS
{
	public interface IPredicate<T>
	{
		bool Matches(T obj);
	}
}
