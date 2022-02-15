//https://github.com/dotnet/runtime/issues/29723
//https://youtu.be/YRcGjsFbx4Y?t=4029

namespace F0.System.Reflection;

public class NullabilityInfoContextTests
{
	private string allowNull = "default";

	[AllowNull]
	public string AllowNull
	{
		get => allowNull;
		set => allowNull = value ?? "default";
	}

	private string? disallowNull;

	[DisallowNull]
	public string? DisallowNull
	{
		get => disallowNull;
		set => disallowNull = value ?? throw new ArgumentNullException(nameof(value));
	}

	[Fact]
	public void NullabilityInfo_AllowNull()
	{
		Type type = GetType();
		PropertyInfo? property = type.GetProperty(nameof(AllowNull));
		Debug.Assert(property is not null);

		NullabilityInfoContext context = new();

		NullabilityInfo info = context.Create(property);

		info.Type.Should().Be(typeof(string));
		info.ReadState.Should().Be(NullabilityState.NotNull);
		info.WriteState.Should().Be(NullabilityState.Nullable);
		info.ElementType.Should().BeNull();
		info.GenericTypeArguments.Should().BeEmpty();
	}

	[Fact]
	public void NullabilityInfo_DisallowNull()
	{
		Type type = GetType();
		PropertyInfo? property = type.GetProperty(nameof(DisallowNull));
		Debug.Assert(property is not null);

		NullabilityInfoContext context = new();

		NullabilityInfo info = context.Create(property);

		info.Type.Should().Be(typeof(string));
		info.ReadState.Should().Be(NullabilityState.Nullable);
		info.WriteState.Should().Be(NullabilityState.NotNull);
		info.ElementType.Should().BeNull();
		info.GenericTypeArguments.Should().BeEmpty();
	}
}
