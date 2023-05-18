//https://github.com/dotnet/runtime/issues/44801

namespace F0.System.Collections.Generic;

public class ListTests
{
	[Fact]
	public void List_1()
	{
		List<int> collection = new(16);

		collection.Capacity.Should().Be(16);

		collection.EnsureCapacity(32).Should().Be(32);
		collection.EnsureCapacity(16).Should().Be(32);

		collection.Capacity.Should().Be(32);

		collection.TrimExcess();

		collection.Capacity.Should().Be(0);
	}

	//Stack<T>
	//Queue<T>
}
