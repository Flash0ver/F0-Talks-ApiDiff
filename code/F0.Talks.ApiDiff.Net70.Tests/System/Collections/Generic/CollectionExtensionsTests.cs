namespace F0.System.Collections.Generic;

public class CollectionExtensionsTests
{
	[Fact]
	public void AsReadOnly_IList_1()
	{
		IList<int> list = Enumerable.Range(0, 3).ToList();

		ReadOnlyCollection<int> readOnly = list.AsReadOnly();

		Assert.Collection(readOnly,
			element => Assert.Equal(0, element),
			element => Assert.Equal(1, element),
			element => Assert.Equal(2, element));

		IList<int> collection = readOnly;

		Assert.Throws<NotSupportedException>(() => collection.Add(3));
		Assert.Throws<NotSupportedException>(() => collection.Remove(1));

		list[0] = 9;

		Assert.Collection(readOnly,
			element => Assert.Equal(9, element),
			element => Assert.Equal(1, element),
			element => Assert.Equal(2, element));
	}

	[Fact]
	public void AsReadOnly_IDictionary_2()
	{
		IDictionary<int, string> dictionary = Enumerable.Range(0, 3).ToDictionary(static key => key, static element => element.ToString(NumberFormatInfo.InvariantInfo));

		ReadOnlyDictionary<int, string> readOnly = dictionary.AsReadOnly();

		Assert.Collection(readOnly,
			kvp => Assert.Equal(KeyValuePair.Create(0, "0"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create(1, "1"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create(2, "2"), kvp));

		IDictionary<int, string> collection = readOnly;

		Assert.Throws<NotSupportedException>(() => collection.Add(3, "3"));
		Assert.Throws<NotSupportedException>(() => collection.Remove(1));

		dictionary[0] = "9";

		Assert.Collection(readOnly,
			kvp => Assert.Equal(KeyValuePair.Create(0, "9"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create(1, "1"), kvp),
			kvp => Assert.Equal(KeyValuePair.Create(2, "2"), kvp));
	}
}
