namespace F0.System.Diagnostics;

/// <summary>
/// <see cref="F0.System.MemoryExtensionsTests"/>
/// </summary>
public class UnreachableExceptionTests
{
	[Theory]
	[InlineData(0, "Empty")]
	[InlineData(1, "Singleton")]
	[InlineData(2, "Pair")]
	[InlineData(3, "Unknown")]
	public void TestCase1(int length, string expected)
	{
		int[] array = new int[length];

		string property = GetProperty(array);

		Assert.Equal(expected, property);

		static string GetProperty(int[] array)
		{
			return array.Length switch
			{
				< 0 => throw new UnreachableException($"{nameof(Array.Length)} cannot be negative."),
				0 => "Empty",
				1 => "Singleton",
				2 => "Pair",
				> 2 => "Unknown",
			};
		}
	}

	[Fact]
	public void TestCase2()
	{
		Type type = typeof(MyClass);

		PropertyInfo? property = type.GetProperty(nameof(MyClass.MyProperty));

		Assert.NotNull(property);
		Assert.True(property.PropertyType == typeof(int));
	}
}

file sealed class MyClass
{
	public int MyProperty => throw new UnreachableException();
}
