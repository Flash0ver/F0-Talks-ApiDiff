namespace F0.System.Diagnostics.CodeAnalysis;

public class RequiresDynamicCodeAttributeTests
{
	[Fact]
	[RequiresDynamicCode("Uses System.Reflection.Emit.", Url = "https://github.com/dotnet/runtime/issues/61239")]
	public void RequiresDynamicCode_Attribute()
	{
		Func<BigInteger> method = EmitMethod();

		BigInteger result = method();

		Assert.Equal(new BigInteger(0x_F0), result);
	}

	[RequiresDynamicCode($"Requires {nameof(DynamicMethod)}.", Url = "https://github.com/dotnet/runtime/issues/61239")]
	private static Func<BigInteger> EmitMethod()
	{
		DynamicMethod method = new("MyMethod", typeof(BigInteger), Type.EmptyTypes, typeof(RequiresDynamicCodeAttributeTests).Module, false);

		ConstructorInfo? constructor = typeof(BigInteger).GetConstructor(new[] { typeof(int) });

		ILGenerator generator = method.GetILGenerator();
		generator.Emit(OpCodes.Ldc_I4, 240);
		generator.Emit(OpCodes.Newobj, constructor!);
		generator.Emit(OpCodes.Ret);

		Func<BigInteger> @delegate = method.CreateDelegate<Func<BigInteger>>();
		return @delegate;
	}

	[Fact]
	[RequiresUnreferencedCode($"Requires {nameof(MethodBase.GetCurrentMethod)}.", Url = "https://learn.microsoft.com/dotnet/api/system.reflection.methodbase.getcurrentmethod")]
	public void RequiresUnreferencedCode_Attribute()
	{
		var method = MethodBase.GetCurrentMethod();

		Assert.NotNull(method);
		Assert.Equal(nameof(RequiresUnreferencedCode_Attribute), method.Name);
	}
}
