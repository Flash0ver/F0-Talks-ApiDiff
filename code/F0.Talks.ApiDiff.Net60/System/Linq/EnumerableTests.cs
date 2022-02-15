namespace F0.System.Linq;

public class EnumerableTests
{
	[Fact]
	public void Chunk()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);

		IEnumerable<int[]> chunks = collection.Chunk(3);

		chunks.Should().SatisfyRespectively(
			_0 => _0.Should().Equal(0, 1, 2),
			_1 => _1.Should().Equal(3, 4, 5),
			_2 => _2.Should().Equal(6, 7, 8),
			_3 => _3.Should().Equal(9)
		);
	}

	[Fact]
	public void DistinctBy()
	{
		Data[] collection = new[]
		{
			new Data("1", 1),
			new Data("2", 1),
			new Data("3", 2),
			new Data("4", 2),
		};

		IEnumerable<Data> distinct = collection.DistinctBy(item => item.Number);

		distinct.Should().Equal(new Data("1", 1), new Data("3", 2));
	}

	//ExceptBy
	//IntersectBy
	//UnionBy	

	[Fact]
	public void ElementAt()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);

		int element = collection.ElementAt(^1);

		element.Should().Be(9);
	}

	[Fact]
	public void ElementAtOrDefault()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);

		int element = collection.ElementAtOrDefault(^0);

		element.Should().Be(0);
	}

	[Fact]
	public void FirstOrDefault_DefaultValue()
	{
		IEnumerable<int> collection = Enumerable.Empty<int>();

		int element = collection.FirstOrDefault(0x_F0);

		element.Should().Be(240);
	}

	//LastOrDefault
	//SingleOrDefault

	[Fact]
	public void Max()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);

		int element = collection.Max(Comparer<int>.Default);

		element.Should().Be(9);
	}

	//Min

	[Fact]
	public void MaxBy()
	{
		Data[] collection = new[]
		{
			new Data("1", 1),
			new Data("2", 1),
			new Data("3", 2),
			new Data("4", 2),
		};

		Data? max = collection.MaxBy(item => item.Number);

		max.Should().Be(new Data("3", 2));
	}

	//MinBy

	[Fact]
	public void Take()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10);

		IEnumerable<int> subsequence = collection.Take(1..^1);

		subsequence.Should().Equal(1, 2, 3, 4, 5, 6, 7, 8);
	}

	[Fact]
	public void TryGetNonEnumeratedCount()
	{
		IEnumerable<int> collection = Enumerable.Range(0, 10).Take(1..^1);

		bool success = collection.TryGetNonEnumeratedCount(out int count);

		success.Should().BeFalse();
		count.Should().Be(0);
	}

	[Fact]
	public void Zip()
	{
		IEnumerable<int> first = Enumerable.Range(0, 3);
		IEnumerable<int> second = Enumerable.Range(3, 3);
		IEnumerable<int> third = Enumerable.Range(6, 3);

		IEnumerable<(int First, int Second, int Third)> zip = first.Zip(second, third);

		zip.Should().SatisfyRespectively(
			_0 => _0.Should().Be((0, 3, 6)),
			_1 => _1.Should().Be((1, 4, 7)),
			_2 => _2.Should().Be((2, 5, 8))
		);
	}

	//IQueryable`1

	private record class Data(string Text, int Number);
}
