using System.Diagnostics;

namespace F0.System;

[ShortRunJob]
public class NullableBenchmarks
{
	private const int Operations = 1_000_000;

	private readonly MyStruct? _value = new(2);

	[Benchmark]
	public int Value()
	{
		Debug.Assert(_value.HasValue);
		int result = 0;

		for (int i = 0; i < Operations; i++)
		{
			MyStruct value = _value.Value;
			result += value.Property;
		}

		return result;
	}

	[Benchmark]
	public int GetValueOrDefault()
	{
		int result = 0;

		for (int i = 0; i < Operations; i++)
		{
			MyStruct value = _value.GetValueOrDefault();
			result += value.Property;
		}

		return result;
	}

	[Benchmark]
	public int GetValueRefOrDefaultRef()
	{
		int result = 0;

		for (int i = 0; i < Operations; i++)
		{
			ref readonly MyStruct value = ref Nullable.GetValueRefOrDefaultRef(in _value);
			result += value.Property;
		}

		return result;
	}

	public struct MyStruct
	{
		private readonly byte _byte1;
		private readonly byte _byte2;
		private readonly byte _byte3;
		private readonly byte _byte4;
		private readonly byte _byte5;
		private readonly byte _byte6;
		private readonly short _int16;
		private readonly long _int64;

		public int Field;

		public MyStruct(int value)
		{
			(Field, Property) = (value, value);
		}

		public int Property { get; set; }
	}
}
