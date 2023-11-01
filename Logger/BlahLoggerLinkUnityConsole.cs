using System;
using UnityEngine;

namespace BlahDebugConsole.Logger
{
internal class BlahLoggerLinkUnityConsole
{
	private readonly bool _useUnityConsoleLevels;
	
	public BlahLoggerLinkUnityConsole(bool useUnityConsoleLevels)
	{
		_useUnityConsoleLevels = useUnityConsoleLevels;
		
		BlahLogger.EvLog += PrintLog;
	}
	
	public void Release()
	{
		BlahLogger.EvLog -= PrintLog;
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private void PrintLog(LogItem log)
	{
		if (_useUnityConsoleLevels)
		{
			var str = $"[{log.Tag}] {log.Msg}";
			switch (log.Type)
			{
				case ELogType.Verbose:
				case ELogType.Info:
					Debug.Log(str);
					break;
				case ELogType.Warning:
					Debug.LogWarning(str);
					break;
				case ELogType.Error:
					Debug.LogError(str);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		else
		{
			var str = $"[{log.Type}][{log.Tag}] {log.Msg}";
			if (log.Type is ELogType.Verbose or ELogType.Info)
				Debug.Log(str);
			else
				Debug.LogWarning(str);
		}
	}
}
}