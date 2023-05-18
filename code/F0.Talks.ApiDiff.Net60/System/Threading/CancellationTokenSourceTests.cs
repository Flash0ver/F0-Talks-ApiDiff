//https://github.com/dotnet/runtime/issues/48492

namespace F0.System.Threading;

public class CancellationTokenSourceTests
{
	[Fact]
	public void TryReset()
	{
		using CancellationTokenSource cancellation = new();
		int state = 0;

		cancellation.Token.Register(() => ++state);

		cancellation.IsCancellationRequested.Should().BeFalse();
		cancellation.TryReset().Should().BeTrue();

		cancellation.Token.Register(() => state += 2);

		state.Should().Be(0);
		cancellation.Cancel();
		state.Should().Be(2);

		cancellation.IsCancellationRequested.Should().BeTrue();
		cancellation.TryReset().Should().BeFalse();
	}
}
