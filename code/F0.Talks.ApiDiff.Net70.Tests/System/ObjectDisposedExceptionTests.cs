namespace F0.System;

public class ObjectDisposedExceptionTests
{
	[Fact]
	public void ThrowIf()
	{
		MyDisposable disposable = new();

		Action act = () => disposable.Method();

		act.Invoke();

		disposable.Dispose();

		var exception = Assert.Throws<ObjectDisposedException>(act);
		Assert.Equal("F0.System.MyDisposable", exception.ObjectName);
	}
}

internal sealed class MyDisposable : IDisposable
{
	private bool _disposed;

	public void Method()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		//ObjectDisposedException.ThrowIf(_disposed, GetType());
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
		}
	}
}
