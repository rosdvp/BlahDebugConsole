using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace BlahDebugConsole.Logger
{
public static class BlahLogger
{
	private static Thread         _mainThread;
	private static bool           _isDelayedLogsTaskRunning;
	private static Queue<LogItem> _delayedLogs;

	private static ELogType _minimalLogType;

	private static Dictionary<Enum, string> _tagToStr;

	private static BlahLoggerLinkUnityConsole _linkUnityConsole;
	private static BlahLoggerLinkFile         _linkFile;
	private static BlahLoggerLinkDelayable    _linkDelayable;

	public static void Init(BlahLoggerConfig config)
	{
		EvLog = null;

		_mainThread               = Thread.CurrentThread;
		_isDelayedLogsTaskRunning = false;
		_delayedLogs              = new Queue<LogItem>();

		_minimalLogType = config.MinimalLogType;

		_tagToStr = new Dictionary<Enum, string>();

		_linkUnityConsole?.Release();
		_linkUnityConsole = new BlahLoggerLinkUnityConsole();

		_linkFile?.Release();
		_linkFile = config.IsWriteIntoFile ? new BlahLoggerLinkFile(config.WriteIntoFileInterval) : null;
		
		_linkDelayable?.Release();
		_linkDelayable = null;
	}

	public static void AttachLinkDelayable(BlahLoggerLinkDelayable link)
	{
		_linkDelayable = link;
	}
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public static event Action<LogItem> EvLog;

	public static void Verb<T>(T tag, string msg) where T : Enum
	{
		if (_minimalLogType <= ELogType.Verbose)
			Log(new LogItem(DateTime.Now, ELogType.Verbose, TagToStr(tag), msg));
	}
	
	public static void Info<T>(T tag, string msg) where T : Enum
	{
		if (_minimalLogType <= ELogType.Info)
			Log(new LogItem(DateTime.Now, ELogType.Info, TagToStr(tag), msg));
	}

	public static void Warn<T>(T tag, string msg) where T : Enum
	{
		if (_minimalLogType <= ELogType.Warning)
			Log(new LogItem(DateTime.Now, ELogType.Warning, TagToStr(tag), msg));
	}

	public static void Err<T>(T tag, string msg) where T : Enum
	{
		if (_minimalLogType <= ELogType.Error)
			Log(new LogItem(DateTime.Now, ELogType.Error, TagToStr(tag), msg));
	}
	
	private static void Log(LogItem log)
	{
		if (Thread.CurrentThread != _mainThread)
		{
			_delayedLogs.Enqueue(log);
			if (!_isDelayedLogsTaskRunning)
			{
				_isDelayedLogsTaskRunning = true;
				AsyncDelayedLogsHandling().Forget();
			}
		}
		else
		{
			EvLog?.Invoke(log);
		}
	}
	
	private static async UniTaskVoid AsyncDelayedLogsHandling()
	{
		if (Thread.CurrentThread != _mainThread)
			await UniTask.Yield(PlayerLoopTiming.Update);
		while (_delayedLogs.TryDequeue(out var log))
			Log(log);
		_isDelayedLogsTaskRunning = false;
	}

	private static string TagToStr<T>(T tag) where T: Enum
	{
		string strTag;
		if (!_tagToStr.TryGetValue(tag, out strTag))
		{
			strTag          = tag.ToString();
			_tagToStr[tag] = strTag;
		}
		return strTag;
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public static void SaveToFile() => _linkFile?.Save();

	public static string GetFilePath() => _linkFile?.GetFilePath();
}
}
