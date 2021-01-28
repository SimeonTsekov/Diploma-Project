using Utils;

namespace Economy.Buildings
{
	public class Fort : Building
	{
		public Fort()
		{
			this.Name = "Fort";
			this.Cost = Constants.BaseBuildingCost;
			this.Built = true;
		}
	}
}