namespace F0.System;

public class ArgumentExceptionTests
{
	[Fact]
	public void ThrowIfNullOrEmpty_Null()
	{
		string? argument = null;

		var act = () => ArgumentException.ThrowIfNullOrEmpty(argument);

		var exception = Assert.Throws<ArgumentNullException>(act);
		Assert.Equal(nameof(argument), exception.ParamName);
	}

	[Fact]
	public void ThrowIfNullOrEmpty_Empty()
	{
		string? argument = String.Empty;

		var act = () => ArgumentException.ThrowIfNullOrEmpty(argument);

		var exception = Assert.Throws<ArgumentException>(act);
		Assert.Equal(nameof(argument), exception.ParamName);
	}

	[Fact]
	public void ThrowIfNullOrEmpty_IsNeitherNullNorEmpty()
	{
		string? argument = "0x_F0";

		var act = () => ArgumentException.ThrowIfNullOrEmpty(argument);

		act.Invoke();
	}
}
