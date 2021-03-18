namespace GlobalDatas
{
	public class NationData
	{
		public string NationId;
		public string Name;
		public string Color;
		public string Capital;
		public int StartingTroops;

		public NationData()
         		{
         		}

		public NationData(string nationId,
			string name,
			string color,
			string capital,
			int startingTroops)
		{
			NationId = nationId;
			Name = name;
			Color = color;
			Capital = capital;
			StartingTroops = startingTroops;
		}
	}
}