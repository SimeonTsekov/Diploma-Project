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
using Random = UnityEngine.Random;

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
        public bool retreating;

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
            retreating = false;
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

        public IEnumerator Move(GameObject destination, int step)
        {
            Debug.Log(nationId + " moving");
            //|| !destination.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(PlayerController.Instance.NationId
            if (!selected && !retreating)
            {
                yield break;
            }

            retreating = false;
            var destinationPosition = destination.transform.position;
            var currentPosition = CurrentProvince.transform.position;
            var hitInfo = new RaycastHit2D();
            var departMoment = TimeController.Instance.Date;
            var arrivalMoment = departMoment.AddDays(step);

            while (currentPosition != destinationPosition)
            {
                while (!TimeController.Instance.Date.Equals(arrivalMoment))
                {
                    yield return null;
                }

                departMoment = TimeController.Instance.Date;
                arrivalMoment = departMoment.AddDays(step);

                coll.enabled = false;
                CurrentProvince.GetComponent<PolygonCollider2D>().enabled = false;
                hitInfo = Physics2D.Linecast(currentPosition, destinationPosition);
                var hitPosition = hitInfo.transform.position;
                CurrentProvince.GetComponent<PolygonCollider2D>().enabled = true;
                coll.enabled = true;
                
                currentPosition = hitPosition;
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

        public void SetStrength()
        {
            strength = troops / Constants.RegimentTroops;
        }

        public void Retreat()
        {
            var provinceName = CurrentProvince.name;
            var controller = GameObject.FindGameObjectsWithTag("Nation")
                .Single(n => n.GetComponent<NationController>().NationId.Equals(nationId))
                .GetComponent<NationController>();

            while (provinceName.Equals(CurrentProvince.name))
            {
                provinceName = controller.ResourceManagement.Provinces[Random.Range(0, controller.ResourceManagement.Provinces.Count)].Name;
            }

            retreating = true;
            StartCoroutine(Move(GameObject.Find(provinceName), Constants.RetreatTravelTime));
        }
    }
}
