using System;
using System.Collections.Generic;
using Economy.Buildings;
using Player;
using UnityEngine;

namespace Economy
{
	public class BuildingManagement : MonoBehaviour
	{
		public List<Building> Buildings;
		
		private void Awake()
		{
			Buildings = new List<Building>();
		}

		public bool Build(Building building)
		{
			Debug.Log(building.GetType());
			Buildings.Add(building);

			return true;
		}
	}
}