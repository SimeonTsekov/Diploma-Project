﻿using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
	public class MonthlyGoldIncreaseLabelController : MonoBehaviour
	{
		private Text _txt;

		private void Start()
		{
			_txt = GetComponent<Text>();
			_txt.text = "";
		}

		private void Update()
		{
			_txt.text = "Monthly: " + PlayerController.Instance.ResourceManagement.GetMonthlyGold().ToString("0.00");
		}
	}
}