using System;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
	public class MoraleBarController : MonoBehaviour
	{
		public Slider moraleSlider;
		private ArmyController _parentArmy;

		private void Awake()
		{
			_parentArmy = GetComponentInParent<ArmyController>();
		}

		private void Update()
		{
			moraleSlider.value = _parentArmy.CurrentMorale;
		}
	}
}