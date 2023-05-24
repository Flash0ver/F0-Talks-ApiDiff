using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

IEnumerable<Summary> summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

string message = String.Join(", ", summaries.Select(static summary => summary.Title));
Console.WriteLine(message);
