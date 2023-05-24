namespace F0.Options;

public sealed class MyConfig
{
	public required DateOnly DateOnly { get; set; }
	public required TimeOnly TimeOnly { get; set; }
	public required Half Half { get; set; }
	public required Int128 Int128 { get; set; }
	public required UInt128 UInt128 { get; set; }
}
