﻿using System.Collections.Generic;
using BlahDebugConsole.Logger;
using UnityEngine;
using UnityEngine.UI;

namespace BlahDebugConsole.Console
{
public class BlahConsole : MonoBehaviour
{
	[Header("Logs Tab")]
	[SerializeField]
	private float _logsFontSize = 25;
	[SerializeField]
	private Color _colorVerbose = new(0.8f, 0.8f, 0.8f, 1);
	[SerializeField]
	private Color _colorInfo = new(1, 1, 1, 1);
	[SerializeField]
	private Color _colorWarning = new(1, 1, 0.3f, 1);
	[SerializeField]
	private Color _colorError = new(1, 0.3f, 0.3f, 1);

	[Header("Actions Tabs")]
	[SerializeField]
	private Vector2 _actionButtonSize = new(200, 100);

	[Header("Components")]
	[SerializeField]
	private BlahConsoleTabsHolder _tabsHolder;
	[SerializeField]
	private BlahConsoleTabLogs _tabLogs;
	[SerializeField]
	private Button _openButton;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private bool _isInited;

	public void Awake()
	{
		if (!_isInited)
			gameObject.SetActive(false);
	}

	public void Init()
	{
		_tabsHolder.Init(_actionButtonSize);
		
		_tabLogs.Init(
			_logsFontSize,
			new Dictionary<ELogType, Color>
			{
				{ ELogType.Verbose, _colorVerbose },
				{ ELogType.Info, _colorInfo },
				{ ELogType.Warning, _colorWarning },
				{ ELogType.Error, _colorError },
			}
		);
		_tabsHolder.AddTab("Logs", _tabLogs.gameObject);

		_openButton.onClick.RemoveAllListeners();
		_openButton.onClick.AddListener(() =>
		{
			_tabsHolder.gameObject.SetActive(!_tabsHolder.gameObject.activeSelf);
		});
		gameObject.SetActive(true);
		
		_isInited = true;
	}

	public BlahConsoleTab AddTab(string tabName) => _tabsHolder.AddTab(tabName);
}
}