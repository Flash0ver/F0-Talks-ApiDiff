namespace F0.System.Text.RegularExpressions;

/// <summary>
/// <see href="https://learn.microsoft.com/dotnet/standard/base-types/regular-expression-source-generators">.NET regular expression source generators</see>
/// </summary>
[SuppressMessage("GeneratedRegex", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Demo")]
public partial class RegexTests
{
	//StringSyntaxAttribute
	//RegexOptions.NonBacktracking
	private static readonly Regex _regex = new("Flash(Over|0ver|OWare)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.NonBacktracking);

	[GeneratedRegex("Flash(Over|0ver|OWare)", RegexOptions.IgnoreCase, "en-US")]
	private static partial Regex GeneratedRegex();

	[Theory]
	[InlineData("FlashOver")]
	[InlineData("flashover")]
	[InlineData("Flash0ver")]
	[InlineData("flash0ver")]
	[InlineData("FlashOWare")]
	[InlineData("flashoware")]
	public void IsMatch_Match_ReturnTrue(string text)
	{
		bool classic = _regex.IsMatch(text);
		bool generated = GeneratedRegex().IsMatch(text);

		Assert.True(classic);
		Assert.True(generated);
	}

	[Theory]
	[InlineData("0x_F0")]
	[InlineData("Backdraft")]
	[InlineData("NDC Oslo 2023")]
	public void IsMatch_Mismatch_ReturnFalse(string text)
	{
		ReadOnlySpan<char> span = text;

		//ReadOnlySpan<char>
		bool classic = _regex.IsMatch(span);
		bool generated = GeneratedRegex().IsMatch(span);

		Assert.False(classic);
		Assert.False(generated);
	}

	[Fact]
	public void Count()
	{
		string text = "https://www.twitch.tv/FlashOWare;https://www.youtube.com/@FlashOWare";
		ReadOnlySpan<char> span = text;

		Assert.Equal(2, _regex.Count(text));
		Assert.Equal(2, _regex.Count(span));

		Assert.Equal(1, _regex.Count(text, text.IndexOf(';')));
		Assert.Equal(1, _regex.Count(span, span.IndexOf(';')));
	}

	[Fact]
	public void EnumerateMatches()
	{
		string text = "https://www.twitch.tv/FlashOWare;https://www.youtube.com/@FlashOWare";
		ReadOnlySpan<char> span = text;

		Regex.ValueMatchEnumerator enumerator = _regex.EnumerateMatches(span, 0);

		Assert.True(enumerator.MoveNext());
		Assert.Equal(22, enumerator.Current.Index);
		Assert.Equal(10, enumerator.Current.Length);

		Assert.True(enumerator.MoveNext());
		Assert.Equal(58, enumerator.Current.Index);
		Assert.Equal(10, enumerator.Current.Length);

		Assert.False(enumerator.MoveNext());
	}
}
