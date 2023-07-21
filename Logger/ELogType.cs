using System.Collections.Generic;

namespace BlahDebugConsole.Logger
{
public enum ELogType
{
	Verbose,
	Info,
	Warning,
	Error
}


public static class LogTypeExtension
{
	private static Dictionary<ELogType, string> _typeToStr = new()
	{
		{ ELogType.Verbose, "verbose" },
		{ ELogType.Info, "info" },
		{ ELogType.Warning, "warning" },
		{ ELogType.Error, "error" },
	};

	public static string Str(this ELogType type) => _typeToStr[type];
}
}