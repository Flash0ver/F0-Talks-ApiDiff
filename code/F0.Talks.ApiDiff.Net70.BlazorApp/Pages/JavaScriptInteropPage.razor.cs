using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace F0.Pages;

public partial class JavaScriptInteropPage
{
	private string? _input;

	public string Result { get; set; } = "Empty";

	private void OnShow()
	{
		Show(_input);
	}

	private void OnWrite()
	{
		Write(_input);
	}

	private void OnRead()
	{
		Result = Read(_input);
	}

	private void OnCallback()
	{
		Callback();
	}
}

public partial class JavaScriptInteropPage
{
	protected override async Task OnInitializedAsync()
	{
		await JSHost.ImportAsync("mymodule", "../Pages/JavaScriptInteropPage.razor.js");
	}
}

[SupportedOSPlatform("browser")]
public partial class JavaScriptInteropPage
{
	[JSImport("show", "mymodule")]
	internal static partial void Show(string? message);

	[JSImport("write", "mymodule")]
	internal static partial void Write(string? data);

	[JSImport("read", "mymodule")]
	internal static partial string Read(string? text, string? defaultText = "default");
}

[SupportedOSPlatform("browser")]
public partial class JavaScriptInteropPage
{
	private static int _counter = 0;

	[JSImport("callback", "mymodule")]
	internal static partial void Callback();

	[JSExport]
	internal static string GetMessage()
	{
		return $"From .NET: {++_counter}";
	}
}
