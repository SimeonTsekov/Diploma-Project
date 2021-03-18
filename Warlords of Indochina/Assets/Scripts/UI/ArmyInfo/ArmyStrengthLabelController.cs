using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ArmyInfo
{
	public class ArmyStrengthLabelController : MonoBehaviour
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
				_txt.text = "Strength: " + ArmyMenuController.Instance.army.strength.ToString("0.00");
			}
			catch (Exception)
			{
			}
		}
	}
}