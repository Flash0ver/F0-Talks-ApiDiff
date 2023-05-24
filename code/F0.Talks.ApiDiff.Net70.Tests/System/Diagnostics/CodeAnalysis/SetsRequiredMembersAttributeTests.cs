namespace F0.System.Diagnostics.CodeAnalysis;

public class SetsRequiredMembersAttributeTests
{
	[Fact]
	public void SetsRequiredMembers_Attribute()
	{
		MyClass instance = new()
		{
			Number = 1,
			Text = "1",
		};

		MyClass copy = new(instance);

		MyClass other = new(2, "2");


		Assert.Equal(instance, copy);
		Assert.NotEqual(instance, other);
	}
}

file sealed record class MyClass
{
	public MyClass()
	{
	}

	[SetsRequiredMembers]
	public MyClass(int number, string text)
	{
		ArgumentNullException.ThrowIfNull(text);

		Number = number;
		Text = text;
	}

	[SetsRequiredMembers]
	public MyClass(MyClass other)
	{
		Number = other.Number;
		Text = other.Text;
	}

	public required int Number { get; init; }
	public required string Text { get; init; }
}
