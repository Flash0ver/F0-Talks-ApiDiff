//Array.MaxLength
//https://github.com/dotnet/runtime/issues/31366
//https://youtu.be/5nMDURGNsQ0?t=299

//Array.Clear(Array)
//https://github.com/dotnet/runtime/issues/51581
//https://youtu.be/wY0t4bkXzS4?t=3727

namespace F0.System;

public class ArrayTests
{
	[Fact]
	public void Clear_Net50()
	{
		int[] array = new[] { 1, 2, 3 };

		Array.Clear(array, 0, array.Length);

		array.Should().Equal(0, 0, 0);
	}

	[Fact]
	public void Clear_Net60()
	{
		int[] array = new[] { 1, 2, 3 };

		Array.Clear(array);

		array.Should().Equal(0, 0, 0);
	}
}
