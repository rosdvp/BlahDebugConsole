using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsoleTab : MonoBehaviour
{
	[SerializeField]
	private GameObject _prefabActionButton;
	[SerializeField]
	private GameObject _actionsButtonsHolder;
	[SerializeField]
	private GridLayoutGroup _actionsButtonsGrid;

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private BlahConsoleHotKeysHandler _hotKeysHandler;
	
	public void Init(BlahConsoleHotKeysHandler hotKeysHandler, Vector2 actionButtonSize)
	{
		_hotKeysHandler              = hotKeysHandler;
		_actionsButtonsGrid.cellSize = actionButtonSize;
	}

	public BlahConsoleTab AddAction(string buttonName, KeyCode hotKey, Action action)
	{
		AddActionImpl(buttonName, hotKey, action);
		return this;
	}

	public BlahConsoleTab AddAction(string buttonName, Action action)
	{
		AddActionImpl(buttonName, null, action);
		return this;
	}

	private void AddActionImpl(string buttonName, KeyCode? rawHotKey, Action action)
	{
		var go = Instantiate(_prefabActionButton, _actionsButtonsHolder.transform);
		var button = go.GetComponent<BlahConsoleActionButton>();
		button.Set(buttonName, rawHotKey, action);
		
		if (rawHotKey is {} hotKey)
			_hotKeysHandler.RegisterHotKey(hotKey, action);
	}
}
}