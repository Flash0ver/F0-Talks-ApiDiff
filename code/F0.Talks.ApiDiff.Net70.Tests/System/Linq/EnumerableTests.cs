namespace F0.System.Linq;

public class EnumerableTests
{
	//Queryable / IQueryable<T>

	private readonly IEnumerable<string> source;

	public EnumerableTests()
	{
		source = new string[] { "Z", "A", "z", "a" };
	}

	[Fact]
	public void Order()
	{
		IOrderedEnumerable<string> ordered = source.Order();

		Assert.Collection(ordered,
			element => Assert.Equal("a", element),
			element => Assert.Equal("A", element),
			element => Assert.Equal("z", element),
			element => Assert.Equal("Z", element));
	}

	[Fact]
	public void Order_IComparer_1()
	{
		IOrderedEnumerable<string> ordered = source.Order(StringComparer.OrdinalIgnoreCase);

		Assert.Collection(ordered,
			element => Assert.Equal("A", element),
			element => Assert.Equal("a", element),
			element => Assert.Equal("Z", element),
			element => Assert.Equal("z", element));
	}

	[Fact]
	public void OrderDescending()
	{
		IOrderedEnumerable<string> ordered = source.OrderDescending();

		Assert.Collection(ordered,
			element => Assert.Equal("Z", element),
			element => Assert.Equal("z", element),
			element => Assert.Equal("A", element),
			element => Assert.Equal("a", element));
	}

	[Fact]
	public void OrderDescending_IComparer_1()
	{
		IOrderedEnumerable<string> ordered = source.OrderDescending(StringComparer.OrdinalIgnoreCase);

		Assert.Collection(ordered,
			element => Assert.Equal("Z", element),
			element => Assert.Equal("z", element),
			element => Assert.Equal("A", element),
			element => Assert.Equal("a", element));
	}
}
