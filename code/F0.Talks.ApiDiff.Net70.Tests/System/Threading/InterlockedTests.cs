namespace F0.System.Threading;

public class InterlockedTests
{
	[Fact]
	public void asd()
	{
		nuint original;
		UIntPtr location1 = 1;

		original = Interlocked.Exchange(ref location1, 2);
		Assert.Equal(UIntPtr.CreateChecked(1), original);
		Assert.Equal(UIntPtr.CreateChecked(2), location1);

		original = Interlocked.Exchange(ref location1, 3);
		Assert.Equal(UIntPtr.CreateChecked(2), original);
		Assert.Equal(UIntPtr.CreateChecked(3), location1);
	}

	[Fact]
	public void CompareExchange()
	{
		nuint original;
		UIntPtr location1 = 1;

		original = Interlocked.CompareExchange(ref location1, 2, 3);
		Assert.Equal(UIntPtr.CreateChecked(1), original);
		Assert.Equal(UIntPtr.CreateChecked(1), location1);

		original = Interlocked.CompareExchange(ref location1, 2, 1);
		Assert.Equal(UIntPtr.CreateChecked(1), original);
		Assert.Equal(UIntPtr.CreateChecked(2), location1);
	}
}
