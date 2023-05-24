namespace F0.System.Runtime.CompilerServices;

public class DisableRuntimeMarshallingAttributeTests
{
	[Fact]
	public void DisableRuntimeMarshalling_Attribute()
	{
		Assert.True(typeof(DisableRuntimeMarshallingAttribute).BaseType == typeof(Attribute));
	}
}
