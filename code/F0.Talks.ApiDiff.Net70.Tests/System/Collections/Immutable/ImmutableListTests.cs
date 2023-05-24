namespace F0.System.Collections.Immutable;

public class ImmutableListTests
{
	[Fact]
	public void Builder_Remove_IEqualityComparer_1()
	{
		var builder = ImmutableList.CreateBuilder<string>();
		builder.AddRange(new[] { "a", "A", "b", "B" });

		bool removed;
		removed = builder.Remove("a", StringComparer.OrdinalIgnoreCase);
		Assert.True(removed);
		removed = builder.Remove("a", StringComparer.OrdinalIgnoreCase);
		Assert.True(removed);
		removed = builder.Remove("a", StringComparer.OrdinalIgnoreCase);
		Assert.False(removed);

		Assert.Collection(builder,
			element => Assert.Equal("b", element),
			element => Assert.Equal("B", element));
	}

	[Fact]
	public void Builder_RemoveRange()
	{
		var builder = ImmutableList.CreateBuilder<int>();
		builder.AddRange(new[] { 1, 2, 3, 4, 5, 6 });

		builder.RemoveRange((IEnumerable<int>)new int[] { 2 });
		Assert.Equal(5, builder.Count);

		builder.RemoveRange((IEnumerable<int>)new int[] { 3 }, EqualityComparer<int>.Default);
		Assert.Equal(4, builder.Count);

		builder.RemoveRange(1, 2);
		Assert.Equal(2, builder.Count);

		Assert.Collection(builder,
			element => Assert.Equal(1, element),
			element => Assert.Equal(6, element));
	}

	[Fact]
	public void Builder_Replace()
	{
		var builder = ImmutableList.CreateBuilder<int>();
		builder.AddRange(new[] { 1, 2, 3 });

		builder.Replace(1, 0);
		builder.Replace(3, 9, EqualityComparer<int>.Default);

		Assert.Collection(builder,
			element => Assert.Equal(0, element),
			element => Assert.Equal(2, element),
			element => Assert.Equal(9, element));
	}
}
