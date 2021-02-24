using System.Collections.Generic;
using Combat;
using Economy;
using GlobalDatas;

namespace Nations
{
	public class AIController : NationController
	{
		public static AIController Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			DontDestroyOnLoad(gameObject);
			ResourceManagement = gameObject.AddComponent<ResourceManagement>();
			CombatController = gameObject.AddComponent<CombatController>();
			NationId = "";
			atWar = new List<string>();
		}

		public void SetNationData(NationData nationData)
		{
			NationData = nationData;
		}
	}
}