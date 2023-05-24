namespace F0.System.Runtime.CompilerServices;

public class RequiredMemberAttributeTests
{
	[Fact]
	public void RequiredMember_Attribute()
	{
		Type type = typeof(MyClass);

		CustomAttributeData? attribute = type.CustomAttributes.SingleOrDefault(static data => data.AttributeType == typeof(RequiredMemberAttribute));

		Assert.NotNull(attribute);
		Assert.Equal(nameof(RequiredMemberAttribute), attribute.AttributeType.Name);
	}
}

file class MyClass
{
	public required string Text { get; set; }
}
