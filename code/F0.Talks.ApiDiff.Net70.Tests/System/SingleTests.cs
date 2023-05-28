namespace F0.System;

public class SingleTests
{
	[Fact]
	public void GenericMath()
	{
		Assert.Equal(MathF.E, Single.E);
		Assert.Equal(MathF.Tau, Single.Tau);
		Assert.Equal(MathF.Tau / 2, Single.Pi);

		Assert.Equal(-0.0f, Single.NegativeZero);
	}
}
