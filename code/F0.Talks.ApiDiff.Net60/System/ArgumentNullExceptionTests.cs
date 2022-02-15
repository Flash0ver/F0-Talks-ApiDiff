//ArgumentNullException.ThrowIfNull(object)
//https://github.com/dotnet/runtime/issues/48573
//https://youtu.be/QuUMh4Xm_EU?t=1071
//https://youtu.be/RqjdL1_lai4?t=44

namespace F0.System;

public class ArgumentNullExceptionTests
{
	[Fact]
	public void ThrowIfNull_Net50()
	{
		Action act = () => Method(null!);

		act.Should().ThrowExactly<ArgumentNullException>()
			.WithParameterName("parameter");

		static void Method(object parameter)
		{
			_ = parameter ?? throw new ArgumentNullException(nameof(parameter));
		}
	}

	[Fact]
	public void ThrowIfNull_Net60()
	{
		Action act = () => Method(null!);

		act.Should().ThrowExactly<ArgumentNullException>()
			.WithParameterName("parameter");

		static void Method(object parameter)
		{
			ArgumentNullException.ThrowIfNull(parameter);
		}
	}
}
