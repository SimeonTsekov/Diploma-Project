using System;
using System.Collections.Generic;
using System.Linq;
using GlobalDatas;
using Provinces;
using UnityEngine;
using Utils;

namespace Economy
{
	public class ResourceManagemnt : MonoBehaviour
	{
		public List<ProvinceData> Provinces;
		public float Gold { get; private set; }
		public int Manpower { get; private set; }
		public int MaximumManpower { get; private set; }

		private void Awake()
		{
			Provinces = new List<ProvinceData>();
			Gold = 0;
			Manpower = 0;
			MaximumManpower = 0;
		}
		
		public void SetProvinces(List<GameObject> provinces)
		{
			foreach (var province in provinces)
			{
				var tempProvince = province.GetComponent<ProvinceController>().ProvinceData;
				this.Provinces.Add(tempProvince);
				Gold += tempProvince.Gold;
				MaximumManpower += tempProvince.Manpower;
			}

			Gold *= Constants.MonthsInAnYear;
			MaximumManpower += Constants.ManpowerIncrease;
			Manpower = (int) Math.Round((double)(MaximumManpower * 3) / 4);
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
			return Provinces.Sum(province => province.Gold);
		}
	}
}