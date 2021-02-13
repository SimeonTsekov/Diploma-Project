using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ArmyInfo
{
	public class ArmyMoraleLabelController : MonoBehaviour
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
				_txt.text = "Morale: " + ArmyMenuController.Instance.army.currentMorale.ToString("0.00")
					+ "/" + ArmyMenuController.Instance.army.maximumMorale.ToString("0.00");
			}
			catch (Exception)
			{
			}
		}
	}
}