namespace F0.Data;

internal static class MyDbContextExtensions
{
	public static async Task ScaffoldAsync(this MyDbContext db, CancellationToken cancellationToken = default)
	{
		IEnumerable<MyRow> entities = Enumerable.Range(1, 10)
			.Select(Create);

		await db.AddRangeAsync(entities, cancellationToken);
		await db.SaveChangesAsync(cancellationToken);
	}

	private static MyRow Create(int number)
	{
		return new MyRow
		{
			Text = number.ToString(NumberFormatInfo.InvariantInfo),
		};
	}
}
