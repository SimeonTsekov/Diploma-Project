using System;
using UI.ProvinceMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ArmyInfo
{
	public class ArmyNationIdLabelController : MonoBehaviour
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
				_txt.text = ArmyMenuController.Instance.army.nationId;
			}
			catch (Exception)
			{
			}
		}
	}
}