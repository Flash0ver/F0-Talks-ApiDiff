namespace F0.System.Reflection.Metadata;

public class MetadataReaderTests
{
	[Fact]
	public void GetAssemblyName()
	{
		AssemblyName assert = MetadataReader.GetAssemblyName("xunit.assert.dll");
		Assert.Equal("xunit.assert", assert.Name);
		Assert.Equal(new Version(2, 4, 2, 0), assert.Version);

		AssemblyName core = AssemblyName.GetAssemblyName("xunit.core.dll");
		Assert.Equal("xunit.core", core.Name);
		Assert.Equal(new Version(2, 4, 2, 0), core.Version);
	}
}
