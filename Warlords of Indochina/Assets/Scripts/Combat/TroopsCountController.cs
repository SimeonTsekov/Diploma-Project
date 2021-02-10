using System;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
	public class TroopsCountController : MonoBehaviour
	{
		private Text _txt;
		public ArmyController parentArmy;
		
		private void Awake()
		{
			_txt = GetComponent<Text>();
		}

		private void Update()
		{
			_txt.text = parentArmy.troops.ToString();
		}
	}
}