using System;

namespace BlahDebugConsole.Logger
{
public readonly struct LogItem
{
	public readonly DateTime Time;
	public readonly ELogType Type;
	public readonly string   Tag;
	public readonly string   Msg;

	public LogItem(DateTime time, ELogType type, string tag, string msg)
	{
		Time = time;
		Type = type;
		Tag  = tag;
		Msg  = msg;
	}
}
}