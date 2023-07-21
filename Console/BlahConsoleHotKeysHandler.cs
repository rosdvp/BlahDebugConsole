using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsoleHotKeysHandler : MonoBehaviour
{
	private List<KeyCode>               _hotKeys        = new();
	private Dictionary<KeyCode, Action> _hotKeyToAction = new();

	public void RegisterHotKey(KeyCode key, Action action)
	{
		if (_hotKeyToAction.ContainsKey(key))
			throw new Exception($"hot key {key} already exists");
		
		_hotKeys.Add(key);
		_hotKeyToAction[key] = action;
	}
	
	private void Update()
	{
		foreach (var key in _hotKeys)
			if (Input.GetKeyDown(key))
				_hotKeyToAction[key].Invoke();
	}
}
}