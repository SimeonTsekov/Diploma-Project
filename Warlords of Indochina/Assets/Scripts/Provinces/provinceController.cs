using System;
using GlobalDatas;
using UI.ProvinceMenu;
using UnityEngine;

namespace Provinces
{
    [RequireComponent(typeof(PolygonCollider2D))]

    public class ProvinceController : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private string _gameObjectName;
        private Color _color;
        private ProvinceData ProvinceData { get; set; }

        void Start()
        {
            _gameObjectName = gameObject.name;
            try
            {
                ProvinceData = ProvinceManagement.Instance.Provinces.Find(x => Equals(x.Name, _gameObjectName));
                ColorUtility.TryParseHtmlString(ProvinceData.Color, out _color);
            }
            catch (Exception e) 
            {
                Debug.Log(e);
            }
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.color = new Color(_color.r, _color.g, _color.b, 0.5f);
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
    }
}
