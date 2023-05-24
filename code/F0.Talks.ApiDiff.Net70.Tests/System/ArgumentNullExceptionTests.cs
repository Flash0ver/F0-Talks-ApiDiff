namespace F0.System;

public class ArgumentNullExceptionTests
{
	[Fact]
	public unsafe void ThrowIfNull_Null()
	{
		void* pointer = null;

		var act = () => ArgumentNullException.ThrowIfNull(pointer);

		var exception = Assert.Throws<ArgumentNullException>(act);
		Assert.Equal(nameof(pointer), exception.ParamName);
	}

	[Fact]
	public unsafe void ThrowIfNull_NotNull()
	{
		int argument = 0x_F0;
		void* pointer = Unsafe.AsPointer(ref argument);

		var act = () => ArgumentNullException.ThrowIfNull(pointer);

		act.Invoke();
	}
}
