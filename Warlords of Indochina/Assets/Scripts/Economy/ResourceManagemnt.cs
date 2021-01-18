using System;
using System.Collections.Generic;
using GlobalDatas;
using Provinces;
using UnityEngine;
using Utils;

namespace Economy
{
	public class ResourceManagemnt : MonoBehaviour
	{
		public List<ProvinceData> Provinces;
		public float gold;
		public int manpower;

		private void Awake()
		{
			Provinces = new List<ProvinceData>();
			gold = 0;
			manpower = 0;
		}
		
		public void SetProvinces(List<GameObject> provinces)
		{
			foreach (var province in provinces)
			{
				var tempProvince = province.GetComponent<ProvinceController>().ProvinceData;
				this.Provinces.Add(tempProvince);
				gold += tempProvince.Gold;
				manpower += tempProvince.Manpower;
			}

			gold *= Constants.MonthsInAnYear;
			Debug.Log(gold);
			manpower += Constants.ManpowerIncrease;
			Debug.Log(manpower);
		}
	}
}