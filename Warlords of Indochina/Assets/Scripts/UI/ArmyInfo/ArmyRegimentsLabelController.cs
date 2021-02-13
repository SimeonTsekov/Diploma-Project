using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ArmyInfo
{
	public class ArmyRegimentsLabelController : MonoBehaviour
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
				_txt.text = "Regiments: " + ArmyMenuController.Instance.army.Regiments.ToString();
			}
			catch (Exception)
			{
			}
		}
	}
}