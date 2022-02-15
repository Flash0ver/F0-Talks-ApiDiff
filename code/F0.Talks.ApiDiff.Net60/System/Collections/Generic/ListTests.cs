namespace F0.System.Collections.Generic;

public class ListTests
{
	[Fact]
	public void List_1()
	{
		List<int> collection = new(16);

		collection.Capacity.Should().Be(16);

		collection.EnsureCapacity(32);
		collection.EnsureCapacity(16);

		collection.Capacity.Should().Be(32);

		collection.TrimExcess();

		collection.Capacity.Should().Be(0);
	}
}
