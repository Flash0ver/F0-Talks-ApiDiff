using System.Reflection.PortableExecutable;

namespace F0.System.Reflection.PortableExecutable;

public class MachineTests
{
	[Fact]
	public void LoongArch32()
	{
		Assert.Equal(25138, (ushort)Machine.LoongArch32);
	}

	[Fact]
	public void LoongArch64()
	{
		Assert.Equal(25188, (ushort)Machine.LoongArch64);
	}
}
