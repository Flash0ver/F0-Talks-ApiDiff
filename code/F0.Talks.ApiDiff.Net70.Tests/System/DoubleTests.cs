namespace F0.System;

public class DoubleTests
{
	[Fact]
	public void GenericMath()
	{
		Assert.Equal(Math.E, Double.E);
		Assert.Equal(Math.Tau, Double.Tau);
		Assert.Equal(Math.Tau / 2, Double.Pi);

		Assert.Equal(-0.0d, Double.NegativeZero);
	}
}
