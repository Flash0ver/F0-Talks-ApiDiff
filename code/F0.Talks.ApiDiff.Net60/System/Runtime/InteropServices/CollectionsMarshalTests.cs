//https://github.com/dotnet/runtime/issues/27062
//https://www.youtube.com/watch?v=_Svjl1-jauY&t=5480s

namespace F0.System.Runtime.InteropServices;

public class CollectionsMarshalTests
{
	[Fact]
	public void GetValueRefOrNullRef()
	{
		Dictionary<int, MyStruct> dictionary = new()
		{
			{ 0, default },
		};

		ref MyStruct value = ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, 0);
		value.Int32 = 255;

		dictionary[0].Int32.Should().Be(255);

		ref MyStruct nullRef = ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, 1);
		Unsafe.IsNullRef(ref nullRef).Should().BeTrue();
		Unsafe.IsNullRef(ref value).Should().BeFalse();
	}

	[Fact]
	public void GetValueRefOrAddDefault()
	{
		Dictionary<int, MyStruct> dictionary = new()
		{
			{ 0, new() { Int32 = 255 } },
		};
		bool exists;

		ref MyStruct value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, 0, out exists);
		exists.Should().BeTrue();
		value.Int32.Should().Be(255);

		ref MyStruct defaultValue = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, 1, out exists);
		exists.Should().BeFalse();
		defaultValue.Int32.Should().Be(0);
	}

	private struct MyStruct
	{
		public byte Byte1 { get; set; }
		public byte Byte2 { get; set; }
		public short Int16 { get; set; }
		public int Int32 { get; set; }
		public long Int64 { get; set; }
	}
}
