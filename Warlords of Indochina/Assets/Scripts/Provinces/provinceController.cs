using System;
using System.Collections.Generic;
using GlobalDatas;
using UI.ProvinceMenu;
using UnityEngine;

namespace Provinces
{
    [RequireComponent(typeof(PolygonCollider2D))]

    public class ProvinceController : MonoBehaviour, IProvinceFetchedListener
    {
        private SpriteRenderer _sprite;
        private string _gameObjectName;
        private Color _color;
        private ProvinceData ProvinceData { get; set; }

        private void Awake()
        {
            _gameObjectName = gameObject.name;
            _sprite = GetComponent<SpriteRenderer>();
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
            ProvinceMenuController.Instance.UpdateProvinceData(this.ProvinceData);
            ProvinceMenuController.Instance.Show();
        }

        public void OnProvincesFetched(List<ProvinceData> provinceDatas)
        {
            Debug.Log(provinceDatas.Count);
            try
            {
                ProvinceData = provinceDatas.Find(x => Equals(x.Name, _gameObjectName));
                Debug.Log("Province set");
                ColorUtility.TryParseHtmlString(ProvinceData.Color, out _color);
            }
            catch (Exception e) 
            {
            }
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
        }
    }
}
