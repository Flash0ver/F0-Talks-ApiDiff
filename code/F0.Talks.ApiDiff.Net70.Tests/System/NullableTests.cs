namespace F0.System;

public class NullableTests
{
	private readonly MyStruct? _field = new(1);
	private readonly MyStruct? _null = null;

	[Fact]
	public unsafe void SizeOf()
	{
		Assert.Equal(24, sizeof(MyStruct));
		Assert.Equal(Environment.Is64BitProcess ? 8 : 4, IntPtr.Size);
	}

	[Fact]
	public void GetValueRefOrDefaultRef()
	{
		MyStruct? local = new(2);

		ref readonly MyStruct valueRef = ref Nullable.GetValueRefOrDefaultRef(in local);
		Assert.Equal(2, valueRef.Property);

		ref readonly MyStruct defaultRef = ref Nullable.GetValueRefOrDefaultRef(in _null);
		Assert.Equal(0, defaultRef.Property);
	}

	[Fact]
	public void GetValueRefOrDefaultRef_Rvalue()
	{
		MyClass instance = new()
		{
			Nullable = 0x_F0,
		};

		ref readonly int valueRef = ref Nullable.GetValueRefOrDefaultRef(instance.Nullable);
		Assert.Equal(240, valueRef);
		instance.Nullable = 7;
		Assert.Equal(240, valueRef);
	}
}

internal struct MyStruct
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

internal class MyClass
{
	public int? Nullable { get; set; }
}
