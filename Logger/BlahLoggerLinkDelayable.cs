using System;
using System.Collections.Generic;

namespace BlahDebugConsole.Logger
{
internal class BlahLoggerLinkDelayable
{
	private readonly List<LogItem>  _delayedLogs = new();

	public BlahLoggerLinkDelayable()
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
	private bool            _isDelaying;

	public void SetListener(Action<LogItem> cb)
	{
		_cb = cb;
	}
	
	public void SetDelaying(bool isDelaying)
	{
		_isDelaying = isDelaying;
		if (!isDelaying)
		{
			if (_delayedLogs.Count > 0)
			{
				for (var i = 0; i < _delayedLogs.Count; i++)
					_cb.Invoke(_delayedLogs[i]);
				_delayedLogs.Clear();
			}
		}
	}
	
	
	private void AddLog(LogItem log)
	{
		if (_isDelaying)
			_delayedLogs.Add(log);
		else
			_cb.Invoke(log);
	}
}
}