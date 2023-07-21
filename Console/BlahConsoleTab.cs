using System;
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
	public void Init(Vector2 actionButtonSize)
	{
		_actionsButtonsGrid.cellSize = actionButtonSize;
	}
	
	public BlahConsoleTab AddAction(string buttonName, Action action)
	{
		var go = Instantiate(_prefabActionButton, _actionsButtonsHolder.transform);
		go.name = $"blah_console_action_{buttonName}";
		go.SetActive(true);
		
		var text   = go.GetComponentInChildren<TextMeshProUGUI>();
		text.text = buttonName;
		
		var button = go.GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => action?.Invoke());

		return this;
	}
}
}