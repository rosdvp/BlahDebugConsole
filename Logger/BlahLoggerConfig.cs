using System;

namespace BlahDebugConsole.Logger
{
[Serializable]
public class BlahLoggerConfig
{
	public ELogType MinimalLogType;
	public bool     IsWriteIntoFile;
	public float    WriteIntoFileInterval;
	public int      CustomListenersCount;
	public bool     UseUnityConsoleLevels;
}
}