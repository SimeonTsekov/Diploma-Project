using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
	public class BuildBuildingButtonController : MonoBehaviour
	{
		private Button _button;
		private Text _text;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_text = GetComponentInChildren<Text>();
		}

		private void Update()
		{
			try
			{
				if (ProvinceMenuController.Instance.province.BuildingManagement.Buildings
					.Exists(b => b.Name.Equals(gameObject.name)))
				{
					_text.text = "Built";
					_button.interactable = false;
				}
				else
				{
					_button.interactable = true;
					_text.text = "Build";
				}
			}
			catch (Exception)
			{
			}
		}
	}
}