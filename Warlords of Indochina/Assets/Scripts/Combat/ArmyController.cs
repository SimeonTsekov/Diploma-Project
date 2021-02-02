using System;
using Player;
using Provinces;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Combat
{
    public class ArmyController : MonoBehaviour
    {
        public string NationId { get; private set; }
        public int Troops { get; private set; }
        public int Regiments { get; private set; }
        public bool selected;

        private void Awake()
        {
            NationId = "";
            Troops = 0;
            Regiments = 0;
            selected = false;
        }

        private void Start()
        {
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OnArmySelect);
        }

        public void InitializeArmy(string nationId, int troops)
        {
            NationId = nationId;
            Troops = troops;
            Regiments = troops / Constants.RegimentTroops;
        }

        private void OnArmySelect()
        {
            if (!NationId.Equals(PlayerController.Instance.NationData.NationId))
            {
                return;
            }
            
            selected = true;
            PlayerController.Instance.Army = gameObject;
        }

        public void Move(GameObject destination)
        {
            if (!selected || !destination.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(PlayerController.Instance.NationId))
            {
                return;
            }

            var destinationPosition = destination.transform.position;
            
            gameObject.transform.position = new Vector3(destinationPosition.x, destinationPosition.y, destinationPosition.z-Constants.ArmyOffset);
        }
    }
}
