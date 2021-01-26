using Utils;

namespace Economy.Buildings
{
	public class Mine : Building
	{
		public int ProductionAmount;

		public Mine()
		{
			this.Name = "Mine";
			this.Cost = Constants.BaseBuildingCost;
			this.ProductionAmount = Constants.BaseMineProduction;
		}
	}
}