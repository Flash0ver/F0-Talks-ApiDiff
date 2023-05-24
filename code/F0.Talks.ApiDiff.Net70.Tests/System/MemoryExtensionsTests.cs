namespace F0.System;

public class MemoryExtensionsTests
{
	[Fact]
	public void CommonPrefixLength()
	{
		ReadOnlySpan<byte> span1 = "NDC London 2023"u8;
		ReadOnlySpan<byte> span2 = "NDC Oslo 2023"u8;

		int length = span1.CommonPrefixLength(span2);

		Assert.Equal(4, length);
	}

	[Fact]
	public void CommonPrefixLength_IEqualityComparer()
	{
		ReadOnlySpan<char> span1 = "ndc london 2023";
		ReadOnlySpan<char> span2 = "NDC OSLO 2023";

		int length = span1.CommonPrefixLength(span2, CharComparer.IgnoreCase);

		Assert.Equal(4, length);
	}

	[Fact]
	public void IndexOfAnyExcept()
	{
		ReadOnlySpan<char> text = "NDC Oslo 2023";

		int index = text.IndexOfAnyExcept('N', 'D', 'C');

		Assert.Equal(3, index);
	}

	[Fact]
	public void IndexOfAnyExcept_ReadOnlySpan()
	{
		ReadOnlySpan<byte> text = "NDC Oslo 2023"u8;

		int index = text.IndexOfAnyExcept("NDC"u8);

		Assert.Equal(3, index);
	}

	[Fact]
	public void LastIndexOfAnyExcept()
	{
		ReadOnlySpan<char> text = "NDC Oslo 2023";

		int index = text.LastIndexOfAnyExcept('0', '2', '3');

		Assert.Equal(8, index);
	}

	[Fact]
	public void LastIndexOfAnyExcept_ReadOnlySpan()
	{
		ReadOnlySpan<byte> text = "NDC Oslo 2023"u8;

		int index = text.LastIndexOfAnyExcept("2023"u8);

		Assert.Equal(8, index);
	}
}

file sealed class CharComparer : IEqualityComparer<char>
{
	public static CharComparer IgnoreCase { get; } = new CharComparer();

	public bool Equals(char x, char y)
	{
		if (Char.IsLetter(x) && Char.IsLetter(y))
		{
			bool xIsLower = Char.IsLower(x);
			bool yIsLower = Char.IsLower(y);
			if (xIsLower ^ yIsLower)
			{
				char left = xIsLower ? Char.ToUpper(x) : x;
				char right = yIsLower ? Char.ToUpper(y) : y;
				return left.Equals(right);
			}
		}

		return x.Equals(y);
	}

	public int GetHashCode(char obj)
	{
		throw new UnreachableException();
	}
}
