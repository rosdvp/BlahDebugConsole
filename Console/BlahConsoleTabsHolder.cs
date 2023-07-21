using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsoleTabsHolder : MonoBehaviour
{
	[SerializeField]
	private GameObject _prefabTabButton;
	[SerializeField]
	private GameObject _tabsButtonsHolder;
	[SerializeField]
	private GameObject _prefabTab;
	[SerializeField]
	private GameObject _tabsHolder;

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private Vector2 _actionButtonSize;
	
	private List<GameObject> _tabs = new();

	public void Init(Vector2 actionButtonSize)
	{
		_actionButtonSize = actionButtonSize;
	}
	
	public BlahConsoleTab AddTab(string tabName)
	{
		var tabGO = Instantiate(_prefabTab, _tabsHolder.transform);
		tabGO.name = $"blah_console_tab_{tabName}";
		_tabs.Add(tabGO);

		AddTab(tabName, tabGO);

		var tab = tabGO.GetComponent<BlahConsoleTab>();
		tab.Init(_actionButtonSize);

		return tab;
	}

	internal void AddTab(string tabName, GameObject tab)
	{
		_tabs.Add(tab);
		
		var buttonGO = Instantiate(_prefabTabButton, _tabsButtonsHolder.transform);
		buttonGO.name = $"blah_console_tab_button_{tabName}";
		buttonGO.SetActive(true);

		var buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>(true);
		buttonText.text = tabName;

		var button = buttonGO.GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => SwitchTab(tab));
	}
	
	private void SwitchTab(GameObject tab)
	{
		foreach (var t in _tabs)
			t.SetActive(t == tab);
	}
}
}