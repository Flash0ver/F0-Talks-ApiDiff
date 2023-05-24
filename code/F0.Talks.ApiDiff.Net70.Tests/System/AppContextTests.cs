namespace F0.System;

public class AppContextTests
{
	[Fact]
	public void SetData()
	{
		object? element;
		element = AppContext.GetData("MyData");
		Assert.Null(element);

		AppDomain.CurrentDomain.SetData("MyData", 1);

		element = AppContext.GetData("MyData");
		Assert.Equal(1, element);

		AppContext.SetData("MyData", 2);
		element = AppContext.GetData("MyData");
		Assert.Equal(2, element);
	}
}
