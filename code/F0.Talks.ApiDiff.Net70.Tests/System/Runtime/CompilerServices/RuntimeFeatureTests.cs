namespace F0.System.Runtime.CompilerServices;

public class RuntimeFeatureTests
{
	[Fact]
	public void ByRefFields()
	{
		Assert.Equal("ByRefFields", RuntimeFeature.ByRefFields);
	}

	[Fact]
	public void NumericIntPtr()
	{
		Assert.Equal("NumericIntPtr", RuntimeFeature.NumericIntPtr);
	}

	[Fact]
	public void VirtualStaticsInInterfaces()
	{
		Assert.Equal("VirtualStaticsInInterfaces", RuntimeFeature.VirtualStaticsInInterfaces);
	}
}
