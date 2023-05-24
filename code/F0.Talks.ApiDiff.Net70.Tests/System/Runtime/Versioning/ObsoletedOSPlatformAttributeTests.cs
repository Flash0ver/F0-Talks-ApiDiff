namespace F0.System.Runtime.Versioning;

public class ObsoletedOSPlatformAttributeTests
{
	[Fact]
	public void ObsoletedOSPlatform_Attribute()
	{
		Assert.True(typeof(ObsoletedOSPlatformAttribute).BaseType == typeof(OSPlatformAttribute));
	}
}

file class MyClass
{
	[SupportedOSPlatform("ios12.0")]
	public void NewApi()
	{
	}

	//ObsoletedOSPlatformAttribute
	[ObsoletedOSPlatform("ios13.0", "API is now Deprecated. Migrate to the new API instead.")]
	public void ObsoleteApi()
	{
	}

	//UnsupportedOSPlatformAttribute(String, String)
	[UnsupportedOSPlatform("ios14.0", "The previously Deprecated API is now Obsolete and will be removed soon. Submitting an App to the Store may be rejected.")]
	public void RemovedApi()
	{
	}

	[UnsupportedOSPlatform("ios15.0")]
	public void UnavailableApi()
	{
	}
}
