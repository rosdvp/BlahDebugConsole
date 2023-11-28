using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsoleActionButton : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;
	[SerializeField]
	private Button _button;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Set(string buttonName, KeyCode? rawHotKey, Action action)
	{
		gameObject.name = $"blah_console_action_{buttonName}";

		if (rawHotKey is { } hotKey)
			buttonName += $"\n[{hotKey.ToString()}]";
		_text.text = buttonName;
		
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(() => action?.Invoke());
		
		gameObject.SetActive(true);
	}
}
}