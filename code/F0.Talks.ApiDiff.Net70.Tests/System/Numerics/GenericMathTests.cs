namespace F0.System.Numerics;

/// <summary>
/// Generic Math
/// <list type="bullet">
/// <item><see langword="static"/> <see langword="virtual"/> members in interfaces</item>
/// <item><see langword="static"/><see langword="abstract"/> members in interfaces</item>
/// <item><see langword="checked"/> user-defined operators</item>
/// <item>Unsigned right-shift operator</item>
/// <item>Relaxed shift operator</item>
/// </list>
/// See also "curiously recurring template pattern" (CRTP)
/// </summary>
public class GenericMathTests
{
	[Fact]
	public void GenericAlgorithm_CheckForOverflowUnderflow()
	{
		Span<MyNumber> span = stackalloc MyNumber[] { new MyNumber(100), new MyNumber(20), new MyNumber(7) };

		MyNumber sum = MyMath.Sum<MyNumber>(span);

		Assert.Equal(new MyNumber(127), sum);
	}

	[Fact]
	public void Unsigned_RightShift_Operator()
	{
		MyNumber number = new(120);

		Assert.Equal("01111000", (number >>> 0).ToBinaryString());
		Assert.Equal("00111100", (number >>> 1).ToBinaryString());
		Assert.Equal("00011110", (number >>> 2).ToBinaryString());
		Assert.Equal("00001111", (number >>> 3).ToBinaryString());
		Assert.Equal("00000111", (number >>> 4).ToBinaryString());
		Assert.Equal("00000011", (number >>> 5).ToBinaryString());
		Assert.Equal("00000001", (number >>> 6).ToBinaryString());
		Assert.Equal("00000000", (number >>> 7).ToBinaryString());
		Assert.Equal("00000000", (number >>> 8).ToBinaryString());
	}
}

file interface IMyNumber<TSelf>
	where TSelf : IMyNumber<TSelf>
{
	static abstract TSelf Zero { get; }
	static abstract TSelf One { get; }
	static abstract TSelf MinusOne { get; }

	static abstract TSelf operator +(TSelf left, TSelf right);
	static virtual TSelf operator checked +(TSelf left, TSelf right) => left + right;
}

file readonly struct MyNumber : IMyNumber<MyNumber>
{
	private readonly sbyte _value;

	public MyNumber(sbyte value) => _value = value;

	public static MyNumber Zero => new MyNumber();
	public static MyNumber One => new MyNumber(1);
	public static MyNumber MinusOne => new MyNumber(-1);

	public static MyNumber operator +(MyNumber left, MyNumber right) => new MyNumber(unchecked((sbyte)(left._value + right._value)));
	public static MyNumber operator checked +(MyNumber left, MyNumber right) => new MyNumber(checked((sbyte)(left._value + right._value)));

	public static MyNumber operator <<(MyNumber value, MyNumber shiftAmount) => new MyNumber((sbyte)(value._value << shiftAmount._value));
	public static MyNumber operator >>(MyNumber value, MyNumber shiftAmount) => new MyNumber((sbyte)(value._value >> shiftAmount._value));
	public static MyNumber operator >>>(MyNumber value, int shiftAmount) => new MyNumber((sbyte)(value._value >>> shiftAmount));

	public override string ToString() => _value.ToString(NumberFormatInfo.InvariantInfo);
	public string ToBinaryString() => Convert.ToString(_value, 2).PadLeft(8, '0');
}

file static class MyMath
{
	public static TResult Sum<TResult>(ReadOnlySpan<TResult> values)
		where TResult : struct, IMyNumber<TResult>
	{
		TResult sum = TResult.Zero;
		foreach (TResult value in values)
		{
			checked { sum += value; }
		}
		return sum;
	}
}
