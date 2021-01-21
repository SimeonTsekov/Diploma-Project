using System;
using System.Collections.Generic;
using System.Linq;
using Economy;
using Economy.Buildings;
using GlobalDatas;
using Player;
using UI.ProvinceMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void Awake()
        {
            _gameObjectName = gameObject.name;
            _sprite = GetComponent<SpriteRenderer>();
            gameObject.tag = "Province";
        }

        private void Start()
        {
            ProvinceManagement.Instance.ProvinceFetchedListeners.Add(this);
        }

        private void OnMouseEnter()
        {
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.75f);
        }

        private void OnMouseExit()
        {
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
        }

        private void OnMouseDown()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            
            if (Equals(currentScene, "MainGameScene"))
            {
                ProvinceMenuController.Instance.UpdateProvinceData(this.ProvinceData);
                ProvinceMenuController.Instance.Show();
            } else if (Equals(currentScene, "NationSelectionScene"))
            {
                PlayerController.Instance.SetNationId(ProvinceData.NationId);
            }
            
            ProvinceMenuController.Instance.Show();
        }

        public void OnProvincesFetched(List<ProvinceData> provinceDatas)
        {
            ProvinceData = provinceDatas.Find(x => Equals(x.Name, _gameObjectName));
            ColorUtility.TryParseHtmlString(ProvinceData.Color, out _color);
            
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
        }

        public void ConstructBuilding(Building building)
        {
            var nation = GameObject
                .FindGameObjectsWithTag("Player")
                .Single(p => p.GetComponent<PlayerController>().NationId.Equals(ProvinceData.NationId)).GetComponent<PlayerController>();
            BuildingManagement.Build(building);
            nation.ResourceManagement.SubstractGold(100);
        }
    }
}
