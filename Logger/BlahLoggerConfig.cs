using System;

namespace BlahDebugConsole.Logger
{
[Serializable]
public class BlahLoggerConfig
{
	public ELogType MinimalLogType;
	public bool     IsWriteIntoFile;
	public float    WriteIntoFileInterval;

	[NonSerialized]
	public bool UseCustomListener;
}
}