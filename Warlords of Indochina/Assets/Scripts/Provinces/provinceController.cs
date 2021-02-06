using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using Economy;
using Economy.Buildings;
using GlobalDatas;
using Player;
using UI.ProvinceMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Provinces
{
    [RequireComponent(typeof(PolygonCollider2D))]

    public class ProvinceController : MonoBehaviour, IProvinceFetchedListener
    {
        private SpriteRenderer _sprite;
        private string _gameObjectName;
        private Color _color;
        public ProvinceData ProvinceData { get; set; }
        public BuildingManagement BuildingManagement { get; private set; }
        private Vector3 position;
        private bool _hovered;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
            _sprite = GetComponent<SpriteRenderer>();
            gameObject.tag = "Province";
            BuildingManagement = gameObject.AddComponent<BuildingManagement>();
            position = gameObject.transform.position;
            _hovered = false;
        }

        private void Start()
        {
            ProvinceManagement.Instance.ProvinceFetchedListeners.Add(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && _hovered)
            {
                StartCoroutine(PlayerController.Instance.Army.GetComponent<ArmyController>().Move(gameObject));
            }
        }

        private void OnMouseEnter()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _sprite.color = new Color(_color.r, _color.g, _color.b, 0.75f);
                _hovered = true;
            }
        }

        private void OnMouseExit()
        {
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
            _hovered = false;
        }

        public void OnMouseDown()
        {
            var currentScene = SceneManager.GetActiveScene().name;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                switch (currentScene)
                {
                    case "MainGameScene":
                        ProvinceMenuController.Instance.UpdateProvinceData(this.ProvinceData);
                        ProvinceMenuController.Instance.Show();
                        break;
                    case "NationSelectionScene":
                        PlayerController.Instance.SetNationId(ProvinceData.NationId);
                        break;
                }
            }

            ProvinceMenuController.Instance.Show();
        }

        public void OnProvincesFetched(List<ProvinceData> provinceDatas)
        {
            ProvinceData = provinceDatas.Find(x => Equals(x.Name, _gameObjectName));
            ColorUtility.TryParseHtmlString(ProvinceData.Color, out _color);
            
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
            BuildingManagement.Buildings = Enumerable.Repeat<Building>(new EmptyBuilding(), ProvinceData.BuildingSlots).ToList();
        }

        public bool ConstructBuilding(Building building, int index)
        {
            var nation = GameObject
                .FindGameObjectsWithTag("Player")
                .Single(p => p.GetComponent<PlayerController>().NationId.Equals(ProvinceData.NationId)).GetComponent<PlayerController>();

            if (nation.ResourceManagement.Gold < building.Cost || Array.Exists<Building>(BuildingManagement.Buildings.ToArray(), b => b.GetType() == building.GetType()))
            {
                return false;
            }
            
            BuildingManagement.Build(building, index);
            nation.ResourceManagement.SubstractGold(building.Cost);
            return true;
        }
    }
}
