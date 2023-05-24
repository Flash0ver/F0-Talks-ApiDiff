//https://github.com/dotnet/runtime/issues/22928
//https://www.youtube.com/watch?v=tr3atnfgUpg
//https://www.youtube.com/watch?v=GtiP3oSyqPw

//https://www.youtube.com/watch?v=mecKToY4JaE&t=354s

namespace F0.System.Collections.Immutable;

public class ImmutableArrayTests
{
	// ImmutableArray

	[Fact]
	public void Create_Span()
	{
		Span<int> items = new int[] { 0, 1 };

		var array = ImmutableArray.Create(items);

		Assert.Collection(array,
			element => Assert.Equal(0, element),
			element => Assert.Equal(1, element));
	}

	[Fact]
	public void Create_ReadOnlySpan()
	{
		ReadOnlySpan<int> items = new int[] { 0, 1 };

		var array = ImmutableArray.Create(items);

		Assert.Collection(array,
			element => Assert.Equal(0, element),
			element => Assert.Equal(1, element));
	}

	[Fact]
	public void ToImmutableArray_Span()
	{
		Span<int> items = new int[] { 0, 1 };

		var array = items.ToImmutableArray();

		Assert.Collection(array,
			element => Assert.Equal(0, element),
			element => Assert.Equal(1, element));
	}

	[Fact]
	public void ToImmutableArray_ReadOnlySpan()
	{
		ReadOnlySpan<int> items = new int[] { 0, 1 };

		var array = items.ToImmutableArray();

		Assert.Collection(array,
			element => Assert.Equal(0, element),
			element => Assert.Equal(1, element));
	}

	// ImmutableArray<T>

	[Fact]
	public void AddRange()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4);

		array = array.AddRange(array, 3);
		Assert.Equal(7, array.Length);

		array = array.AddRange(new ReadOnlySpan<int>(5));
		Assert.Equal(8, array.Length);

		array = array.AddRange(9, 10, 11);
		Assert.Equal(11, array.Length);

		array = array.AddRange(new int[] { 12, 13, 14 }, 2);
		Assert.Equal(13, array.Length);
	}

	[Fact]
	public void AddRange_Derived()
	{
		var array = ImmutableArray.Create<Type>(typeof(ImmutableArrayTests));
		var other = ImmutableArray.Create<TypeInfo>(typeof(ImmutableArrayTests).GetTypeInfo());

		array = array.AddRange(other);
		Assert.Equal(2, array.Length);

		array = array.AddRange(new TypeInfo[] { GetType().GetTypeInfo() });
		Assert.Equal(3, array.Length);
	}

	[Fact]
	public void InsertRange()
	{
		var array = ImmutableArray.Create(1);

		array = array.InsertRange(0, new ReadOnlySpan<int>(2));
		Assert.Equal(2, array.Length);

		array = array.InsertRange(1, new int[] { 3, 4 });
		Assert.Equal(4, array.Length);

		Assert.Collection(array,
			element => Assert.Equal(2, element),
			element => Assert.Equal(3, element),
			element => Assert.Equal(4, element),
			element => Assert.Equal(1, element));
	}

	[Fact]
	public void RemoveRange()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4, 5);

		array = array.RemoveRange(new ReadOnlySpan<int>(2), EqualityComparer<int>.Default);
		Assert.Equal(4, array.Length);

		array = array.RemoveRange(new int[] { 3, 4 }, EqualityComparer<int>.Default);
		Assert.Equal(2, array.Length);

		Assert.Collection(array,
			element => Assert.Equal(1, element),
			element => Assert.Equal(5, element));
	}

	[Fact]
	public void AsSpan()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4);

		ReadOnlySpan<int> span = array.AsSpan(1, 2);
		Assert.Equal(2, span.Length);
		Assert.Equal(2, span[0]);
		Assert.Equal(3, span[1]);
	}

	[Fact]
	public void AsSpan_Range()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4);

		ReadOnlySpan<int> span = array.AsSpan(1..^1);
		Assert.Equal(2, span.Length);
		Assert.Equal(2, span[0]);
		Assert.Equal(3, span[1]);
	}

	[Fact]
	public void Slice()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4);

		array = array.Slice(1, 2);

		Assert.Collection(array,
			element => Assert.Equal(2, element),
			element => Assert.Equal(3, element));
	}

	[Fact]
	public void CopyTo_Span()
	{
		var array = ImmutableArray.Create(1, 2, 3, 4);

		Span<int> destination = stackalloc int[8];
		destination.Clear();

		array.CopyTo(destination);

		Assert.Equal(8, destination.Length);
		Assert.Equal(1, destination[0]);
		Assert.Equal(2, destination[1]);
		Assert.Equal(3, destination[2]);
		Assert.Equal(4, destination[3]);
		Assert.Equal(0, destination[4]);
		Assert.Equal(0, destination[5]);
		Assert.Equal(0, destination[6]);
		Assert.Equal(0, destination[7]);
	}

	// ImmutableArray<T>.Builder

	[Fact]
	public void Builder_AddRange()
	{
		var builder = ImmutableArray.CreateBuilder<int>();

		ReadOnlySpan<int> items = new int[] { 1, 2, 3 };
		builder.AddRange(items);

		Assert.Collection(builder,
			element => Assert.Equal(1, element),
			element => Assert.Equal(2, element),
			element => Assert.Equal(3, element));
	}

	[Fact]
	public void Builder_AddRange_Derived()
	{
		var builder = ImmutableArray.CreateBuilder<Type>();

		ReadOnlySpan<TypeInfo> items = new TypeInfo[] { typeof(ImmutableArrayTests).GetTypeInfo() };
		builder.AddRange(items);

		Assert.Collection(builder,
			element => Assert.Equal(typeof(ImmutableArrayTests), element));
	}

	[Fact]
	public void Builder_InsertRange()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.Add(1);

		builder.InsertRange(0, (IEnumerable<int>)new[] { 2 });
		Assert.Equal(2, builder.Count);

		builder.InsertRange(1, ImmutableArray.Create(3, 4));
		Assert.Equal(4, builder.Count);

		Assert.Collection(builder,
			element => Assert.Equal(2, element),
			element => Assert.Equal(3, element),
			element => Assert.Equal(4, element),
			element => Assert.Equal(1, element));
	}

	[Fact]
	public void Builder_RemoveRange()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3, 4, 5, 6);

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
	public void Builder_Remove_IEqualityComparer_1()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3);

		bool removed = builder.Remove(2, EqualityComparer<int>.Default);
		Assert.True(removed);

		Assert.Collection(builder,
			element => Assert.Equal(1, element),
			element => Assert.Equal(3, element));
	}

	[Fact]
	public void Builder_RemoveAll()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3, 4);

		builder.RemoveAll(static element => Int32.IsEvenInteger(element));

		Assert.Collection(builder,
			element => Assert.Equal(1, element),
			element => Assert.Equal(3, element));
	}

	[Fact]
	public void Builder_Replace()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3);

		builder.Replace(1, 0);
		builder.Replace(3, 9, EqualityComparer<int>.Default);

		Assert.Collection(builder,
			element => Assert.Equal(0, element),
			element => Assert.Equal(2, element),
			element => Assert.Equal(9, element));
	}

	[Fact]
	public void Builder_IndexOf()
	{
		var builder = ImmutableArray.CreateBuilder<string>();
		builder.AddRange("A", "A", "B");

		int index = builder.IndexOf("a", 1, StringComparer.Ordinal);
		Assert.Equal(-1, index);

		index = builder.IndexOf("a", 1, StringComparer.OrdinalIgnoreCase);
		Assert.Equal(1, index);
	}

	[Fact]
	public void Builder_CopyTo_Span()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3, 4);

		Span<int> destination = stackalloc int[8];
		destination.Clear();

		builder.CopyTo(destination);

		Assert.Equal(8, destination.Length);
		Assert.Equal(1, destination[0]);
		Assert.Equal(2, destination[1]);
		Assert.Equal(3, destination[2]);
		Assert.Equal(4, destination[3]);
		Assert.Equal(0, destination[4]);
		Assert.Equal(0, destination[5]);
		Assert.Equal(0, destination[6]);
		Assert.Equal(0, destination[7]);
	}

	[Fact]
	public void Builder_CopyTo_Array()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3, 4);

		var destination = new int[8];
		builder.CopyTo(destination);

		Assert.Equal(8, destination.Length);
		Assert.Equal(1, destination[0]);
		Assert.Equal(2, destination[1]);
		Assert.Equal(3, destination[2]);
		Assert.Equal(4, destination[3]);
		Assert.Equal(0, destination[4]);
		Assert.Equal(0, destination[5]);
		Assert.Equal(0, destination[6]);
		Assert.Equal(0, destination[7]);
	}

	[Fact]
	public void Builder_CopyTo_Int32_Array_Int32_Int32()
	{
		var builder = ImmutableArray.CreateBuilder<int>();
		builder.AddRange(1, 2, 3, 4, 5);

		var destination = new int[8];

		builder.CopyTo(1, destination, 2, 3);

		Assert.Equal(8, destination.Length);
		Assert.Equal(0, destination[0]);
		Assert.Equal(0, destination[1]);
		Assert.Equal(2, destination[2]);
		Assert.Equal(3, destination[3]);
		Assert.Equal(4, destination[4]);
		Assert.Equal(0, destination[5]);
		Assert.Equal(0, destination[6]);
		Assert.Equal(0, destination[7]);
	}
}
