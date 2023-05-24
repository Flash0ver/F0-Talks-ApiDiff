namespace F0.System.Collections.Immutable;

public class ImmutableSortedSetTests
{
	[Fact]
	public void Builder_IndexOf()
	{
		var builder = ImmutableSortedSet.CreateBuilder<int>();
		_ = builder.Add(3);
		_ = builder.Add(4);
		_ = builder.Add(5);
		_ = builder.Add(6);

		int index;
		index = builder.IndexOf(1);
		Assert.Equal(-1, index);

		index = builder.IndexOf(9);
		Assert.Equal(-5, index);

		index = builder.IndexOf(6);
		Assert.Equal(3, index);
	}
}
