namespace F0.System;

public class DecimalTests
{
	[Fact]
	public void Scale()
	{
		Assert.Equal((byte)0, (0m).Scale);
		Assert.Equal((byte)1, (0.1m).Scale);
		Assert.Equal((byte)28, (0.1234567890123456789012345678m).Scale);
		Assert.Equal((byte)28, (0.12345678901234567890123456789m).Scale);
	}
}
