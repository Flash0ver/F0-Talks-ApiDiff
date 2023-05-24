namespace F0.System.Runtime.CompilerServices;

public class CompilerFeatureRequiredAttributeTests
{
	[Fact]
	public void RefStructs()
	{
		Assert.Equal("RefStructs", CompilerFeatureRequiredAttribute.RefStructs);
	}

	[Fact]
	public void RequiredMembers()
	{
		Assert.Equal("RequiredMembers", CompilerFeatureRequiredAttribute.RequiredMembers);
	}
}
