namespace F0.System;

public class MemoryTests
{
	[Fact]
	public void Span_Ctor()
	{
		int number = 0x_F0;

		Span<int> span = new(ref number);

		Assert.Equal(1, span.Length);
		Assert.Equal(240, span[0]);

		number = 2023;

		Assert.Equal(2023, span[0]);
	}

	[Fact]
	public void ReadOnlySpan_Ctor()
	{
		int number = 0x_F0;

		ReadOnlySpan<int> span = new(in number);

		Assert.Equal(1, span.Length);
		Assert.Equal(240, span[0]);

		number = 2023;

		Assert.Equal(2023, span[0]);
	}

	[Fact]
	public void ReadOnlySpan_Ctor_Rvalue()
	{
		ReadOnlySpan<int> span = new(0x_F0);

		Assert.Equal(1, span.Length);
		Assert.Equal(240, span[0]);
	}
}
