using System;
using System.Collections.Generic;

namespace BlahDebugConsole.Logger
{
internal class BlahLoggerLinkCustom
{
	private readonly List<LogItem> _delayedLogs = new();

	public BlahLoggerLinkCustom()
	{
		BlahLogger.EvLog += AddLog;
	}

	public void Release()
	{
		BlahLogger.EvLog -= AddLog;
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private Action<LogItem> _cb;

	public void SetListener(Action<LogItem> cb)
	{
		_cb = cb;

		if (_cb != null)
			if (_delayedLogs.Count > 0)
			{
				for (var i = 0; i < _delayedLogs.Count; i++)
					_cb.Invoke(_delayedLogs[i]);
				_delayedLogs.Clear();
			}
	}
    
	private void AddLog(LogItem log)
	{
		if (_cb == null)
			_delayedLogs.Add(log);
		else
			_cb.Invoke(log);
	}
}
}