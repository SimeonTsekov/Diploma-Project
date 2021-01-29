using UI.ProvinceMenu;
using UnityEngine;
using Utils;

namespace Economy.Buildings
{
	public class Barracks : Building
	{
		public float ProductionAmount;

		public Barracks()
		{
			this.Name = "Barracks";
			this.Cost = Constants.BaseBuildingCost;
			this.ProductionAmount = Constants.BaseProduction;
			this.Built = true;
			IncreaseManpowerInProvince();
		}
		
		private void IncreaseManpowerInProvince()
		{
			ProvinceMenuController.Instance.province.ProvinceData.Manpower +=
				(int)(ProvinceMenuController.Instance.province.ProvinceData.Manpower * (ProductionAmount / 100));
		}
	}
}