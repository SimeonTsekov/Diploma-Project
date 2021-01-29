using System;
using System.Collections.Generic;
using System.Linq;
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
		}

		public bool Build(Building building, int index)
		{
			Buildings.Insert(index, building);

			return true;
		}
	}
}