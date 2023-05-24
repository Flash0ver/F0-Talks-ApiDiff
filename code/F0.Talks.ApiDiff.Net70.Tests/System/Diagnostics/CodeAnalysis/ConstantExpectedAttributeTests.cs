namespace F0.System.Diagnostics.CodeAnalysis;

public class ConstantExpectedAttributeTests
{
	[Fact]
	public void ConstantExpected_Attribute()
	{
		int value = Get(0x_F0);

		Assert.Equal(240, value);
	}

	public static int Get([ConstantExpected(Min = 1, Max = 128)] int value)
	{
		return value;
	}

	[Fact]
	public void Sum_Sse2()
	{
		if (Sse2.IsSupported)
		{
			var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			int sum = Sum(numbers);

			Assert.Equal(45, sum);
		}
		else
		{
			throw new NotSupportedException($"{typeof(Sse2)}");
		}
	}

	// https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/
	public static unsafe int Sum(ReadOnlySpan<int> numbers)
	{
		int result;

		fixed (int* pointer = numbers)
		{
			Vector128<int> vector = Vector128<int>.Zero;

			int index = 0;
			int lastBlockIndex = numbers.Length - (numbers.Length % 4);

			while (index < lastBlockIndex)
			{
				vector = Sse2.Add(vector, Sse2.LoadVector128(pointer + index));
				index += 4;
			}

			vector = Sse2.Add(vector, Sse2.Shuffle(vector, 0x4E));
			vector = Sse2.Add(vector, Sse2.Shuffle(vector, 0xB1));

			result = vector.ToScalar();

			while (index < numbers.Length)
			{
				result += pointer[index];
				index += 1;
			}
		}

		return result;
	}
}
