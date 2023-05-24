namespace F0.System.Runtime.InteropServices;

public class ArchitectureTests
{
	[Fact]
	public void LoongArch64()
	{
		Assert.Equal(6, (int)Architecture.LoongArch64);
		Assert.NotEqual(Architecture.LoongArch64, RuntimeInformation.ProcessArchitecture);
	}

	[Fact]
	public void Armv6()
	{
		Assert.Equal(7, (int)Architecture.Armv6);
		Assert.NotEqual(Architecture.Armv6, RuntimeInformation.ProcessArchitecture);
	}

	[Fact]
	public void Ppc64le()
	{
		Assert.Equal(8, (int)Architecture.Ppc64le);
		Assert.NotEqual(Architecture.Ppc64le, RuntimeInformation.ProcessArchitecture);
	}
}
