namespace Economy.Buildings
{
	public abstract class Building
	{
		public string Name { get; protected set; }
		public int Cost { get; protected set; }
		public bool Built{ get; protected set; }
	}
}