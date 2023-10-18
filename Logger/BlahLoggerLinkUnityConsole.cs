using System;
using UnityEngine;

namespace BlahDebugConsole.Logger
{
internal class BlahLoggerLinkUnityConsole
{
	public BlahLoggerLinkUnityConsole()
	{
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
}
}