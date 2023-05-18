//https://github.com/dotnet/runtime/issues/40475

namespace F0.System.Threading;

public class CancellationTokenTests
{
	[Fact]
	public void Register_CancellationToken()
	{
		using CancellationTokenSource cancellation = new();
		CancellationToken cancellationToken = cancellation.Token;

		cancellationToken.Register((object? state, CancellationToken token) =>
		{
			state.Should().Be("state");
			token.Should().Be(cancellationToken);
		}, "state");

		cancellation.Cancel();
	}
}
