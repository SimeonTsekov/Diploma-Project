using UI.ProvinceMenu;
using Utils;

namespace Economy.Buildings
{
	public class Mine : Building
	{
		public float ProductionAmount;

		public Mine()
		{
			this.Name = "Mine";
			this.Cost = Constants.BaseBuildingCost;
			this.ProductionAmount = Constants.BaseProduction;
			this.Built = true;
			IncreaseGoldProductionInProvince();
		}

		private void IncreaseGoldProductionInProvince()
		{
			ProvinceMenuController.Instance.province.ProvinceData.Gold +=
				ProvinceMenuController.Instance.province.ProvinceData.Gold * (ProductionAmount / 100);
		}
	}
}