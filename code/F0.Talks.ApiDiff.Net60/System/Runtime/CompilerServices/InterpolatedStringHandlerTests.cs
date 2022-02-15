//https://github.com/dotnet/runtime/issues/50601
//https://youtu.be/_lSOebMGeo8?t=70

//https://github.com/dotnet/runtime/issues/50635
//https://youtu.be/RQfgeW0cOFk?t=117

//https://github.com/dotnet/runtime/issues/51962
//https://youtu.be/Cquc3gfSirQ?t=6076

//https://github.com/dotnet/runtime/issues/57538
//https://youtu.be/ztrfSfgXjFU?t=194

//https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/debug-assert-conditional-evaluation

using System.Text;

namespace F0.System.Runtime.CompilerServices;

public class InterpolatedStringHandlerTests
{
	[Fact]
	public void DefaultInterpolatedStringHandler()
	{
		int number = 0x_F0;

		string interpolated = $"Literal {number} Literal";

		interpolated.Should().Be("Literal 240 Literal");
	}

	[Fact]
	public void StringBuilder()
	{
		StringBuilder builder = new();

		_ = builder.Append($"Text {0x_F0}.");

		builder.ToString().Should().Be("Text 240.");
	}

	[Fact]
	public void MemoryExtensions_SpanT_True()
	{
		Span<char> span = stackalloc char[10];

		bool success = span.TryWrite($"Text {678}.", out int charsWritten);

		success.Should().BeTrue();
		span[7].Should().Be('8');
		span[8].Should().Be('.');
		charsWritten.Should().Be(9);
	}

	[Fact]
	public void MemoryExtensions_SpanT_False()
	{
		Span<char> span = stackalloc char[6];

		bool success = span.TryWrite($"Text {678}.", out int charsWritten);

		success.Should().BeFalse();
		span[0].Should().Be('T');
		span[1].Should().Be('e');
		span[2].Should().Be('x');
		span[3].Should().Be('t');
		charsWritten.Should().Be(0);
	}

	[Fact]
	public void Debug_Assert_Net50()
	{
		IsDebug().Should().BeTrue();

		int number = 0;
		Debug.Assert(true, (string)$"{number++}");

		number.Should().Be(1);
	}

	[Fact]
	public void Debug_Assert_Net60()
	{
		IsDebug().Should().BeTrue();

		int number = 0;
		Debug.Assert(true, $"{number++}");

		number.Should().Be(0);
	}

	private static bool IsDebug()
	{
#if DEBUG
		return true;
#else
		return false;
#endif
	}
}
