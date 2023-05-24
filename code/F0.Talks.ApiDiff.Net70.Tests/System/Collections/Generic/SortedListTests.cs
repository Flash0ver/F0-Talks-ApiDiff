namespace F0.System.Collections.Generic;

public class SortedListTests
{
	SortedList<char, string> _list;

	public SortedListTests()
	{
		_list = new SortedList<char, string>(Comparer<char>.Default)
		{
			{ 'b', "B" },
			{ 'a', "A" },
			{ 'd', "D" },
		};
	}

	[Fact]
	public void GetKeyAtIndex()
	{
		char key = _list.GetKeyAtIndex(2);
		Assert.Equal('d', key);

		_list.Add('c', "C");

		key = _list.GetKeyAtIndex(2);
		Assert.Equal('c', key);
	}

	[Fact]
	public void GetValueAtIndex()
	{
		string value = _list.GetValueAtIndex(2);
		Assert.Equal("D", value);

		_list.Add('c', "C");

		value = _list.GetValueAtIndex(2);
		Assert.Equal("C", value);
	}

	[Fact]
	public void SetValueAtIndex()
	{
		Assert.Collection(_list,
			kvp => Assert.Equal(KeyValuePair.Create('a', "A"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create('b', "B"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create('d', "D"), kvp));

		_list.SetValueAtIndex(2, "C");

		Assert.Collection(_list,
			kvp => Assert.Equal(KeyValuePair.Create('a', "A"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create('b', "B"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create('d', "C"), kvp));
	}
}
