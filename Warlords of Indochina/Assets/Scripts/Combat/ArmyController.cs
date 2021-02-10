using System;
using System.Collections;
using System.Linq;
using GlobalDatas;
using Nations;
using Player;
using Provinces;
using TimeControl;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Combat
{
    public class ArmyController : MonoBehaviour
    {
        public string nationId;
        public int troops;
        public int Regiments { get; private set; }
        public bool selected;
        public bool stopped;
        public GameObject CurrentProvince { get; private set; }
        public bool fighting;
        private Collider coll;
        public float maximumMorale;
        public float currentMorale;
        public int strength;

        private void Awake()
        {
            nationId = "";
            troops = 0;
            Regiments = 0;
            selected = false;
            stopped = true;
            fighting = false;
            coll = GetComponent<Collider>();
            maximumMorale = Constants.MaximumMorale;
            currentMorale = maximumMorale;
        }

        private void Start()
        {
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(OnArmySelect);
        }

        public void SetCurrentProvince()
        {
            CurrentProvince = GameObject.FindGameObjectsWithTag("Nation")
                .Single(n => n.GetComponent<NationController>().NationId.Equals(nationId))
                .GetComponent<NationController>().Capital;
        }

        public void InitializeArmy(string nationId, int troops, Color color)
        {
            this.nationId = nationId;
            this.troops = troops;
            Regiments = troops / Constants.RegimentTroops;
            strength = Regiments;
            gameObject.GetComponentInChildren<Image>().color = color;
        }

        private void OnArmySelect()
        {
            if (!nationId.Equals(PlayerController.Instance.NationData.NationId))
            {
                return;
            }
            
            selected = true;
            PlayerController.Instance.Army = gameObject;
        }

        public IEnumerator Move(GameObject destination)
        {
            //|| !destination.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(PlayerController.Instance.NationId
            if (!selected)
            {
                yield break;
            }

            var destinationPosition = destination.transform.position;
            var currentPosition = CurrentProvince.transform.position;
            var hitInfo = new RaycastHit2D();
            var departMoment = TimeController.Instance.Date;
            var arrivalMoment = departMoment.AddDays(Constants.TravelTime);

            while (currentPosition != destinationPosition)
            {
                while (!TimeController.Instance.Date.Equals(arrivalMoment))
                {
                    yield return null;
                }

                departMoment = TimeController.Instance.Date;
                arrivalMoment = departMoment.AddDays(Constants.TravelTime);

                coll.enabled = false;
                CurrentProvince.GetComponent<PolygonCollider2D>().enabled = false;
                hitInfo = Physics2D.Linecast(currentPosition, destinationPosition);
                CurrentProvince.GetComponent<PolygonCollider2D>().enabled = true;
                coll.enabled = true;
                
                currentPosition = hitInfo.transform.position;
                CurrentProvince = GameObject.FindGameObjectsWithTag("Province")
                    .Single(p => p.transform.position.Equals(currentPosition));
                
                gameObject.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z-Constants.ArmyOffset);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Army") && !fighting)
            {
                StartCoroutine(GameStateController.Instance.Battle(gameObject, other.gameObject));
            }
        }

        public void SetFighting(bool fighting)
        {
            this.fighting = fighting;
        }
    }
}
