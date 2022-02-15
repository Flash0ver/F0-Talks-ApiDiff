//Random.Shared
//https://github.com/dotnet/runtime/issues/43887
//https://youtu.be/vNPybpatlUU?t=6314

namespace F0.System;

public class RandomTests
{
	[Fact]
	public void Shared_Net60()
	{
		int value = Random.Shared.Next();

		value.Should().BeGreaterThanOrEqualTo(0).And.BeLessThan(Int32.MaxValue);
	}
}
