using System;
using TimeControl;
using UnityEngine;

namespace UI
{
	public class SettingsMenuController : MonoBehaviour
	{
		public GameObject settingsMenu;
		private bool settingsMenuOpened;

		private void Awake()
		{
			settingsMenuOpened = false;
			settingsMenu.SetActive(settingsMenuOpened);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ChangeState();
			}
		}

		private void ChangeState()
		{
			TimeController.Instance.OnPause();
			settingsMenuOpened = !settingsMenuOpened;
			settingsMenu.SetActive(settingsMenuOpened);
		}

		public void OnQuit()
		{
			Application.Quit();
		}

		public void OnSave()
		{
			GameStateController.Instance.OnSave();
		}
	}
}