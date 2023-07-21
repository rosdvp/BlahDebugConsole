using System.Collections.Generic;
using BlahDebugConsole.Logger;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsoleTabLogs : MonoBehaviour
{
	[Header("Components")]
	[SerializeField]
	private TextMeshProUGUI _text;
	[SerializeField]
	private Button _shareFileButton;

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private readonly Dictionary<ELogType, string> _typeToColor = new();

	public void Init(float logsTextFontSize, Dictionary<ELogType, Color> typeToColor)
	{
		_text.fontSize = logsTextFontSize;
		_text.text     = "";
		
		_typeToColor.Clear();
		foreach (var pair in typeToColor)
			_typeToColor[pair.Key] = "#" + ColorUtility.ToHtmlStringRGBA(pair.Value);

		BlahLogger.EvLog -= AddLog;
		BlahLogger.EvLog += AddLog;
		
		_shareFileButton.onClick.RemoveAllListeners();
		_shareFileButton.onClick.AddListener(OnShareTap);
	}
	
	private void OnDestroy()
	{
		BlahLogger.EvLog -= AddLog;
		_shareFileButton.onClick.RemoveAllListeners();
	}
	
	private void AddLog(LogItem log)
	{
		_text.text += $"\n<color={_typeToColor[log.Type]}>[{log.Time:mm:ss}][{log.Type}][{log.Tag}] {log.Msg}</color>";
	}

	private void OnShareTap()
	{
		string filePath = BlahLogger.GetFilePath();
		if (string.IsNullOrEmpty(filePath))
			return;
		
		BlahLogger.SaveToFile();

#if UNITY_EDITOR
		UnityEditor.EditorUtility.RevealInFinder(filePath);
#elif UNITY_ANDROID
		using var unity    = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using var activity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		using var sharer = new AndroidJavaObject("com.blah.shareplugin.Sharer");
		sharer.Call("Share", activity, filePath, "text/*");
#endif
	}
}
}