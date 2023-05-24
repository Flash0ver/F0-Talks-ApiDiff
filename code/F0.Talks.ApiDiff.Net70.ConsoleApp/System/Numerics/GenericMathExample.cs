namespace F0.System.Numerics;

internal static class GenericMathExample
{
	private const int MaxStackBytes = 1_024;
	private const NumberStyles Style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;

	public static unsafe TNumber Sum<TNumber>(ReadOnlySpan<string> args)
		where TNumber : unmanaged, INumber<TNumber>
	{
		int size = sizeof(TNumber);

		Span<TNumber> numbers = args.Length * size is >= 0 and <= MaxStackBytes
			? stackalloc TNumber[args.Length]
			: new TNumber[args.Length];

		for (int i = 0; i < args.Length; i++)
		{
			string arg = args[i];
			var number = TNumber.Parse(arg, Style, NumberFormatInfo.InvariantInfo);
			numbers[i] = number;
		}

		return Sum<TNumber>(numbers);
	}

	// https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/
	private static TNumber Sum<TNumber>(ReadOnlySpan<TNumber> numbers)
		where TNumber : unmanaged, INumber<TNumber>
	{
		var result = TNumber.Zero;
		int currentIndex = 0;

		if (Vector<TNumber>.IsSupported && numbers.Length >= Vector<TNumber>.Count)
		{
			var vector = Vector<TNumber>.Zero;
			int upperBound = numbers.Length - (numbers.Length % Vector<TNumber>.Count);

			while (currentIndex < upperBound)
			{
				vector += new Vector<TNumber>(numbers.Slice(currentIndex));
				currentIndex += Vector<TNumber>.Count;
			}

			for (int index = 0; index < Vector<TNumber>.Count; index++)
			{
				result += vector[index];
			}
		}

		Debug.Assert(currentIndex <= numbers.Length);

		while (currentIndex < numbers.Length)
		{
			result += numbers[currentIndex];
			currentIndex += 1;
		}

		return result;
	}
}
