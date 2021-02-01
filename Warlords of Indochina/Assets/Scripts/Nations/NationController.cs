using System.Linq;
using Combat;
using Economy;
using GlobalDatas;
using Provinces;
using UnityEngine;

namespace Nations
{
	public abstract class NationController : MonoBehaviour
	{
		public string NationId { get; protected set; }
		public NationData NationData { get; set; }
		public ResourceManagement ResourceManagement { get; protected set; }
		public CombatController CombatController { get; protected set; }
		public GameObject armyPrefab;
		public GameObject Army { get; protected set; }
		public GameObject Capital { get; private set; }

		public void CreateArmy()
		{
			Army = Instantiate(armyPrefab, GameObject.FindGameObjectWithTag("Canvas").transform, false);
			Army.transform.position = Capital.transform.position;
			Army.GetComponent<ArmyController>().InitializeArmy(NationId, 10000);
		}

		public void SetNationId(string nationId)
		{
			NationId = nationId;
		}

		public void SetCapital()
		{
			Capital = GameObject.FindGameObjectsWithTag("Province")
				.Single(p => p.GetComponent<ProvinceController>().ProvinceData.Name.Equals(NationData.Capital));
		}
	}
}