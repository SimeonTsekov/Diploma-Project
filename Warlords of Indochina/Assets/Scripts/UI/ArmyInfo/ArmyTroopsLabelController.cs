using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ArmyInfo
{
	public class ArmyTroopsLabelController : MonoBehaviour
	{
		private Text _txt;

		private void Start()
		{
			_txt = GetComponent<Text>();
			_txt.text = "";
		}

		private void Update()
		{
			try
			{
				_txt.text = "Troops: " + ArmyMenuController.Instance.army.troops.ToString();
			}
			catch (Exception)
			{
			}
		}
	}
}