namespace F0.System.Runtime.InteropServices
{
	using MyNamespace;

	internal static partial class PInvokeGeneratorExample
	{
		public static void RunILJit()
		{
			int value = PlatformInvokes.MessageBoxW(IntPtr.Zero, $"via {typeof(DllImportAttribute)}", "Hei, Oslo!", 0);
			MyConsole.WriteLineGreen($"Return value: {value}");
			MyConsole.WriteLineCyan($"LastPInvokeErrorMessage: {Marshal.GetLastPInvokeErrorMessage()}");
		}

		public static void RunGenerator()
		{
			int value = PlatformInvokes.MessageBoxWGenerator(IntPtr.Zero, $"via {typeof(LibraryImportAttribute)}", "Hei, Oslo!", 0);
			MyConsole.WriteLineGreen($"Return value: {value}");
			MyConsole.WriteLineCyan($"LastPInvokeErrorMessage: {Marshal.GetLastPInvokeErrorMessage()}");
		}

		public static void New()
		{
			_ = typeof(DisableRuntimeMarshallingAttribute);

			_ = typeof(AnsiStringMarshaller);
			_ = typeof(ArrayMarshaller<,>);
			_ = typeof(BStrStringMarshaller);
			_ = typeof(ContiguousCollectionMarshallerAttribute);
			_ = typeof(CustomMarshallerAttribute);
			_ = typeof(MarshalMode);
			_ = typeof(MarshalUsingAttribute);
			_ = typeof(NativeMarshallingAttribute);
			_ = typeof(PointerArrayMarshaller<,>);
			_ = typeof(ReadOnlySpanMarshaller<,>);
			_ = typeof(SpanMarshaller<,>);
			_ = typeof(Utf16StringMarshaller);
			_ = typeof(Utf8StringMarshaller);
		}
	}
}

namespace MyNamespace
{
	internal static partial class PlatformInvokes
	{
		[DllImport("user32.dll")]
		internal static extern int MessageBoxW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpText, [MarshalAs(UnmanagedType.LPWStr)] string lpCaption, uint uType);

		[LibraryImport("user32.dll", EntryPoint = "MessageBoxW")]
		internal static partial int MessageBoxWGenerator(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpText, [MarshalAs(UnmanagedType.LPWStr)] string lpCaption, uint uType);
	}
}
