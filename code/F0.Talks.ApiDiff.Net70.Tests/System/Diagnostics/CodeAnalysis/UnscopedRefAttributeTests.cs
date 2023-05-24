namespace F0.System.Diagnostics.CodeAnalysis;

public class UnscopedRefAttributeTests
{
	[Fact]
	public void UnscopedRefAttribute_Attribute_1()
	{
		MyStruct instance = new();

		ref int value = ref instance.Property;
		value = 1;

		Assert.Equal(1, instance.Property);
	}

	[Fact]
	public void UnscopedRefAttribute_Attribute_2()
	{
		ref int number = ref Methods.Return(out int value);

		Assert.True(Unsafe.AreSame(ref value, ref number));
	}

	[Fact]
	public void UnscopedRefAttribute_Attribute_3()
	{
		ref char value = ref Methods.Method();

		Assert.True(Unsafe.IsNullRef(ref value));
	}
}

file static class Methods
{
	public static ref MyStruct Return(ref MyStruct value)
	{
		return ref value;
	}

	public static ref RefStruct Return(ref RefStruct value)
	{
		return ref value;
	}

	public static ref int Return([UnscopedRef] out int value)
	{
		value = 1;
		return ref value;
	}

	public static ref char Method()
	{
		//char[] array = new[] { '@', '0', 'x', '_', 'F', '0' };
		Span<char> span = stackalloc char[6] { '@', '0', 'x', '_', 'F', '0' };

		RefStruct instance = new();
		ref RefStruct value = ref instance;
		value.Escape(span);

		return ref instance.Reference;
	}
}

file struct MyStruct
{
	private int _field;

	[UnscopedRef]
	public ref int Property => ref _field;

	[UnscopedRef]
	public ref int GetField() => ref _field;
}

file ref struct RefStruct
{
	public ref char Reference;
	public char Value;
	public int Length;

	public void Escape(scoped Span<char> buffer)
	{
		ref char first = ref MemoryMarshal.GetReference(buffer);
		//Reference = ref MemoryMarshal.GetReference(buffer);
		Value = first;

		Length = buffer.Length;
	}
}
