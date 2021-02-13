using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using GlobalDatas;
using Nations;
using Provinces;
using UnityEngine;
using Utils;

namespace Economy
{
	public class ResourceManagement : MonoBehaviour
	{
		public List<ProvinceData> Provinces { get; private set; }
		public float Gold { get; internal set; }
		public int Manpower { get; internal set; }
		private int MaximumManpower { get; set; }

		private void Awake()
		{
			Provinces = new List<ProvinceData>();
			Gold = 0;
			Manpower = 0;
			MaximumManpower = 0;
		}
		
		public List<GameObject> SetProvinces(List<GameObject> provinces)
		{
			foreach (var province in provinces)
			{
				var tempProvince = province.GetComponent<ProvinceController>().ProvinceData;
				this.Provinces.Add(tempProvince);
				//Gold += tempProvince.Gold;
				MaximumManpower += tempProvince.Manpower;
			}

			//Gold *= Constants.MonthsInAnYear;
			Gold = 1000;
			MaximumManpower += Constants.ManpowerIncrease;
			Manpower = (int) Math.Round((double)(MaximumManpower * 3) / 4);

			return provinces;
		}

		public void UpdateResources()
		{
			Gold += GetMonthlyGold();
			Manpower += Manpower == MaximumManpower ? 0 : GetMonthlyManpowerRecovery();
		}

		public int GetMonthlyManpowerRecovery()
		{
			return (Constants.ManpowerIncrease + Provinces.Sum(province => province.Manpower)) /
			       Constants.ManpowerRecoverySpeedDivider;
		}

		public float GetMonthlyGold()
		{
			var army = gameObject.GetComponentInParent<NationController>()
				.Army.GetComponent<ArmyController>();

			var armyCost = Constants.RegimentMonthlyCostConstant * army.strength * Constants.RegimentCost;
			
			return Provinces.Sum(province => province.Gold) - armyCost;
		}

		public void SubstractGold(int amount)
		{
			Gold -= amount;
		}
	}
}