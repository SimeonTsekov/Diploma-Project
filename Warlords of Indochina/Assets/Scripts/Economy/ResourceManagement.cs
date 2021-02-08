using System;
using System.Collections.Generic;
using System.Linq;
using GlobalDatas;
using Provinces;
using UnityEngine;
using Utils;

namespace Economy
{
	public class ResourceManagement : MonoBehaviour
	{
		private List<ProvinceData> _provinces;
		public float Gold { get; private set; }
		public int Manpower { get; private set; }
		private int MaximumManpower { get; set; }

		private void Awake()
		{
			_provinces = new List<ProvinceData>();
			Gold = 0;
			Manpower = 0;
			MaximumManpower = 0;
		}
		
		public List<GameObject> SetProvinces(List<GameObject> provinces)
		{
			foreach (var province in provinces)
			{
				var tempProvince = province.GetComponent<ProvinceController>().ProvinceData;
				this._provinces.Add(tempProvince);
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
			return (Constants.ManpowerIncrease + _provinces.Sum(province => province.Manpower)) /
			       Constants.ManpowerRecoverySpeedDivider;
		}

		public float GetMonthlyGold()
		{
			return _provinces.Sum(province => province.Gold);
		}

		public void SubstractGold(int amount)
		{
			Gold -= amount;
		}
	}
}