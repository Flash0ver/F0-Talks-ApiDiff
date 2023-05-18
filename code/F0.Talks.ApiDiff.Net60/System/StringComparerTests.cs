//https://github.com/dotnet/runtime/issues/50059

namespace F0.System;

public class StringComparerTests
{
	[Fact]
	public void IsWellKnownOrdinalComparer()
	{
		bool ignoreCase;
		Dictionary<string, int> dictionary = new(StringComparer.Ordinal);

		StringComparer.IsWellKnownOrdinalComparer(dictionary.Comparer!, out ignoreCase)
			.Should().BeTrue();
		ignoreCase.Should().BeFalse();

		dictionary = new(StringComparer.OrdinalIgnoreCase);

		StringComparer.IsWellKnownOrdinalComparer(dictionary.Comparer!, out ignoreCase)
			.Should().BeTrue();
		ignoreCase.Should().BeTrue();
	}

	[Fact]
	public void IsWellKnownCultureAwareComparer()
	{
		CompareInfo? compareInfo;
		CompareOptions compareOptions;
		HashSet<string> set = new(StringComparer.CurrentCulture);

		StringComparer.IsWellKnownCultureAwareComparer(set.Comparer!, out compareInfo, out compareOptions)
			.Should().BeTrue();
		compareInfo.Should().BeSameAs(CultureInfo.CurrentCulture.CompareInfo);
		compareOptions.Should().Be(CompareOptions.None);

		set = new(StringComparer.CurrentCultureIgnoreCase);

		StringComparer.IsWellKnownCultureAwareComparer(set.Comparer!, out compareInfo, out compareOptions)
			.Should().BeTrue();
		compareInfo.Should().BeSameAs(CultureInfo.CurrentCulture.CompareInfo);
		compareOptions.Should().Be(CompareOptions.IgnoreCase);
	}
}
