namespace F0.System;

public class EnumTests
{
	[Fact]
	public void GetValuesAsUnderlyingType()
	{
		Array constants = Enum.GetValuesAsUnderlyingType(typeof(StringSplitOptions));

		AssertStringSplitOptions(constants);
	}

	[Fact]
	public void GetValuesAsUnderlyingType_Generic()
	{
		Array constants = Enum.GetValuesAsUnderlyingType<StringSplitOptions>();

		AssertStringSplitOptions(constants);
	}

	[Fact]
	public void GetEnumValuesAsUnderlyingType()
	{
		Type type = typeof(MyEnum);

		Array constants = type.GetEnumValuesAsUnderlyingType();

		AssertMyEnum(constants);
	}

	private static void AssertStringSplitOptions(Array constants)
	{
		Assert.Equal(3, constants.Length);
		Assert.Equal(0, constants.GetValue(0));
		Assert.Equal(1, constants.GetValue(1));
		Assert.Equal(2, constants.GetValue(2));
		Assert.IsType<int>(constants.GetValue(0));
		Assert.IsType<int>(constants.GetValue(1));
		Assert.IsType<int>(constants.GetValue(2));
	}

	private static void AssertMyEnum(Array constants)
	{
		Assert.Equal(2, constants.Length);
		Assert.Equal((byte)0, constants.GetValue(0));
		Assert.Equal((byte)1, constants.GetValue(1));
		Assert.IsType<byte>(constants.GetValue(0));
		Assert.IsType<byte>(constants.GetValue(1));
	}
}

file enum MyEnum : byte
{
	Zero,
	One,
}
