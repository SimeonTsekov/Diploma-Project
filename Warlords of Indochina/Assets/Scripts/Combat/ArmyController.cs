using System;
using System.Linq;
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
        public bool stopped;
        public GameObject CurrentProvince { get; private set; }

        private void Awake()
        {
            NationId = "";
            Troops = 0;
            Regiments = 0;
            selected = false;
            stopped = true;
        }

        private void Start()
        {
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OnArmySelect);
        }

        public void SetCurrentProvince()
        {
            CurrentProvince = GameObject.FindGameObjectsWithTag("Nation")
                .Single(n => n.GetComponent<PlayerController>().NationId.Equals(NationId))
                .GetComponent<PlayerController>().Capital;
            
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
            var currentPosition = CurrentProvince.transform.position;
            var hitInfo = new RaycastHit2D();

            /*while (currentPosition != destinationPosition)
            {
                if (Physics.Linecast(currentPosition, destinationPosition, out hitInfo, layerMask, QueryTriggerInteraction.Collide))
                {
                    currentPosition = hitInfo.transform.position;
                    Debug.Log(currentPosition);
                }
            }*/

            CurrentProvince.GetComponent<PolygonCollider2D>().enabled = false;
            CurrentProvince.GetComponent<PolygonCollider2D>().enabled = true;
            
            currentPosition = hitInfo.transform.position;
            CurrentProvince = GameObject.FindGameObjectsWithTag("Province")
                .Single(p => p.transform.position.Equals(currentPosition));

            gameObject.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z-Constants.ArmyOffset);
        }
    }
}
