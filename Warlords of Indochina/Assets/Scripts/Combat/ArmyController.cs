using System;
using System.Collections;
using System.Linq;
using GlobalDatas;
using Nations;
using Player;
using Provinces;
using TimeControl;
using UI.ArmyInfo;
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
        public int Regiments { get; internal set; }
        public bool selected;
        public bool stopped;
        public GameObject CurrentProvince { get; private set; }
        public bool fighting;
        private Collider coll;
        public float maximumMorale;
        public float currentMorale;
        public int strength;
        public bool retreating;
        public bool besieging;
        public bool moving;
        public Coroutine movement;

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
            besieging = false;
            moving = false;
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
            ArmyMenuController.Instance.Show();
            ArmyMenuController.Instance.SetArmyInformation(this);
        }

        public IEnumerator Move(GameObject destination, int step)
        {
            moving = true;
            besieging = false;

            var owner = GameObject.FindGameObjectsWithTag("Nation")
                .Single(n => n.GetComponent<NationController>().NationData.NationId.Equals(nationId))
                .GetComponent<NationController>();

            if (!selected && !retreating
                          || (!owner.atWar.Contains(destination.GetComponent<ProvinceController>().ProvinceData.NationId))
                          && !owner.NationData.NationId.Equals(nationId))
            {
                moving = false;
                yield break;
            }

            retreating = false;
            var destinationPosition = destination.transform.position;
            var destinationController = destination.GetComponent<ProvinceController>();
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

            moving = false;
            if (!destinationController.ProvinceData.NationId.Equals(nationId))
            {
                StartCoroutine(GameStateController.Instance.Siege(this, destinationController));
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

            if (controller.ResourceManagement.Provinces.Count == 1)
            {
                Destroy(gameObject);
                return;
            }
            
            while (provinceName.Equals(CurrentProvince.name))
            {
                provinceName = controller.ResourceManagement.Provinces[Random.Range(0, controller.ResourceManagement.Provinces.Count)].Name;
            }

            retreating = true;
            StartCoroutine(Move(GameObject.Find(provinceName), Constants.RetreatTravelTime));
        }

        public void ResetMorale()
        {
            currentMorale = maximumMorale * Constants.VictoryMoraleRecovery;
        }

        public void RestoreMonthlyMorale()
        {
            if (fighting || retreating || Math.Abs(currentMorale - maximumMorale) < 0.00001) return;
            
            currentMorale += maximumMorale * Constants.BaseMonthlyMoraleRecovery;
            
            if (CurrentProvince.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(nationId))
            {
                currentMorale += maximumMorale * Constants.BaseMonthlyMoraleRecoveryOnFriendlyTerritory;

                if (currentMorale > maximumMorale)
                {
                    currentMorale = maximumMorale;
                }
            }
        }

        public void RestoreStrength()
        {
            strength = troops / Constants.RegimentTroops;
        }
    }
}
