namespace F0.System;

public class CharTests
{
	[Fact]
	public void IsAsciiDigit()
	{
		bool yes = Char.IsAsciiDigit('0');
		bool no = Char.IsAsciiDigit('O');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiHexDigit()
	{
		bool yes = Char.IsAsciiHexDigit('f');
		bool no = Char.IsAsciiHexDigit('g');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiHexDigitLower()
	{
		bool yes = Char.IsAsciiHexDigitLower('f');
		bool no = Char.IsAsciiHexDigitLower('F');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiHexDigitUpper()
	{
		bool yes = Char.IsAsciiHexDigitUpper('F');
		bool no = Char.IsAsciiHexDigitUpper('f');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiLetter()
	{
		bool yes = Char.IsAsciiLetter('F');
		bool no = Char.IsAsciiLetter('0');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiLetterLower()
	{
		bool yes = Char.IsAsciiLetterLower('f');
		bool no = Char.IsAsciiLetterLower('F');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiLetterOrDigit()
	{
		bool yes = Char.IsAsciiLetterOrDigit('f');
		bool no = Char.IsAsciiLetterOrDigit('_');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsAsciiLetterUpper()
	{
		bool yes = Char.IsAsciiLetterUpper('F');
		bool no = Char.IsAsciiLetterUpper('f');

		Assert.True(yes);
		Assert.False(no);
	}

	[Fact]
	public void IsBetween()
	{
		bool yes = Char.IsBetween('f', 'a', 'f');
		bool no = Char.IsBetween('g', 'a', 'f');

		Assert.True(yes);
		Assert.False(no);
	}
}
