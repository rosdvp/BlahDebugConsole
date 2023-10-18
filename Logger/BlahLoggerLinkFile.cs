using System.IO;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BlahDebugConsole.Logger
{
internal class BlahLoggerLinkFile
{
	private const string FILE_NAME = "logs.txt";

	private readonly string _filePath;
	
	private readonly StringBuilder _sb = new();

	private readonly CancellationTokenSource _intervalSaveCTS;

	public BlahLoggerLinkFile(float saveInterval)
	{
		_filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

		_sb.AppendLine();
		_sb.AppendLine("---new session---");

		BlahLogger.EvLog += AddToBuilder;

		if (!Mathf.Approximately(saveInterval, 0))
		{
			_intervalSaveCTS = new CancellationTokenSource();
			IntervalSaveTask(saveInterval, _intervalSaveCTS.Token).Forget();
		}
	}
	
	public void Release()
	{
		BlahLogger.EvLog -= AddToBuilder;
		
		_intervalSaveCTS?.Cancel();
		_intervalSaveCTS?.Dispose();
	}
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private void AddToBuilder(LogItem log)
	{
		_sb.AppendLine($"[{log.Time:HH:mm:ss}][{log.Type.Str()}][{log.Tag}] {log.Msg}");
	}

	public string GetFilePath() => _filePath;
	
	public void Save()
	{
		File.AppendAllText(_filePath, _sb.ToString());
		_sb.Clear();
	}
	
	private async UniTask IntervalSaveTask(float interval, CancellationToken cancellationToken)
	{
		while (!cancellationToken.IsCancellationRequested)
		{
			await UniTask.WaitForSeconds(interval, cancellationToken: cancellationToken);
			Save();
		}
	}
}
}