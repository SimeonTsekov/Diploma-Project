using System;
using GlobalDatas;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class BuildBuildingButtonController : MonoBehaviour
    {
        private int _slotNum;
        private Button _button;
        private void Start()
        {
            _slotNum = int.Parse(gameObject.name);
            _button = gameObject.GetComponent<Button>();
        }

        private void Update()
        {
            _button.interactable = _slotNum <= ProvinceMenuController.Instance.ProvinceData.BuildingSlots;
        }
    }
}
