using System;
using System.Collections.Generic;

namespace BlahDebugConsole.Logger
{
public class BlahLoggerLinkDelayable
{
	private readonly Action<LogItem> _cbLog;
	private readonly List<LogItem>  _delayedLogs = new();
	
	public BlahLoggerLinkDelayable(Action<LogItem> cbLog)
	{
		_cbLog = cbLog;

		BlahLogger.EvLog += AddLog;
	}
	
	public void Release()
	{
		BlahLogger.EvLog -= AddLog;
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private bool _isDelaying;

	public void SetDelaying(bool isDelaying)
	{
		_isDelaying = isDelaying;
		if (!isDelaying)
		{
			if (_delayedLogs.Count > 0)
			{
				for (var i = 0; i < _delayedLogs.Count; i++)
					_cbLog.Invoke(_delayedLogs[i]);
				_delayedLogs.Clear();
			}
		}
	}
	
	
	private void AddLog(LogItem log)
	{
		if (_isDelaying)
			_delayedLogs.Add(log);
		else
			_cbLog.Invoke(log);
	}
}
}