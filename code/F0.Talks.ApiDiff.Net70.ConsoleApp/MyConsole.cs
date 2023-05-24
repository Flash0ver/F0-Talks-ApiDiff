namespace F0;

internal static class MyConsole
{
	private static void WriteLine(string value, ConsoleColor color)
	{
		ConsoleColor previous = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.WriteLine(value);
		Console.ForegroundColor = previous;
	}

	public static void WriteLineRed(string value)
	{
		WriteLine(value, ConsoleColor.Red);
	}

	public static void WriteLineGreen(string value)
	{
		WriteLine(value, ConsoleColor.Green);
	}

	public static void WriteLineCyan(string value)
	{
		WriteLine(value, ConsoleColor.Cyan);
	}
}
