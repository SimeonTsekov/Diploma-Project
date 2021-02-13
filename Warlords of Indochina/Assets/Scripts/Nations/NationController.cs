using System;
using System.Linq;
using Combat;
using Economy;
using GlobalDatas;
using Provinces;
using UnityEngine;
using Utils;

namespace Nations
{
	public abstract class NationController : MonoBehaviour
	{
		public string NationId { get; protected set; }
		public NationData NationData { get; set; }
		public ResourceManagement ResourceManagement { get; protected set; }
		public CombatController CombatController { get; protected set; }
		public GameObject armyPrefab;
		public GameObject Army { get; set; }
		public GameObject Capital { get; private set; }

		public void CreateArmy()
		{
			var capitalPosition = Capital.transform.position;

			armyPrefab = Resources.Load("army") as GameObject;
			Army = Instantiate(armyPrefab, GameObject.FindGameObjectWithTag("Canvas").transform, false);
			Army.transform.position = new Vector3(capitalPosition.x, capitalPosition.y, capitalPosition.z-Constants.ArmyOffset);
			ColorUtility.TryParseHtmlString(NationData.Color, out var color);
			Army.GetComponent<ArmyController>().InitializeArmy(NationData.NationId, NationData.StartingTroops, color);
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

		public void RefillArmy()
		{
			if (Army.Equals(null))
			{
				return;
			}
			
			var armyController = Army.GetComponent<ArmyController>();
			var reinforcements = armyController.Regiments * Constants.MonthlyReinforcements;

			if (ResourceManagement.Manpower < reinforcements)
			{
				return;
			}
			
			if (armyController.troops < armyController.Regiments * Constants.RegimentTroops)
			{
				armyController.troops = armyController.Regiments * Constants.RegimentTroops;
				ResourceManagement.Manpower -= reinforcements;
			}

			if (armyController.troops > armyController.Regiments * Constants.RegimentTroops)
			{
				armyController.troops = armyController.Regiments * Constants.RegimentTroops;
			}
		}
	}
}