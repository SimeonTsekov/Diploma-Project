﻿using System.Linq;
using Economy.Buildings;
using GlobalDatas;
using Provinces;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace UI.ProvinceMenu
{
    public class ProvinceMenuController : MonoBehaviour
    {
        public static ProvinceMenuController Instance { get; private set; }
        public ProvinceData ProvinceData;
        public RectTransform menuTransform;
        private bool _isHidden;
        public int currentSlot;
        private bool _buildingSlotsPanelActive;
        private bool _buildingsPanelActive;
        public GameObject buildingSlotsPanel;
        public GameObject buildingsPanel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            currentSlot = 0;
            _isHidden = true;
            _buildingSlotsPanelActive = false;
            _buildingsPanelActive = false;
            buildingsPanel.SetActive(_buildingsPanelActive);
            buildingSlotsPanel.SetActive(_buildingSlotsPanelActive);
        }

        public void UpdateProvinceData(ProvinceData provinceData)
        {
            this.ProvinceData = provinceData;
        }
        
        public void Show()
        {
            if (!_isHidden) return;
            _isHidden = false;
            menuTransform.anchorMax = Vector2.zero;
            menuTransform.anchorMin = Vector2.zero;
        }

        public void ShowBuildingSlots()
        {
            _buildingSlotsPanelActive = !_buildingSlotsPanelActive;
            buildingSlotsPanel.SetActive(_buildingSlotsPanelActive);
        }

        public void ShowBuildings(Button button)
        {
            var clickedSlot = int.Parse(button.name);

            if (clickedSlot != currentSlot && _buildingsPanelActive)
            {
                currentSlot = clickedSlot;
                return;
            }
            
            currentSlot = clickedSlot;
            _buildingsPanelActive = !_buildingsPanelActive;
            buildingsPanel.SetActive(_buildingsPanelActive);
        }

        public void OnBuild(Button button)
        {
            var building = button.name;
            var province = GameObject.FindGameObjectsWithTag("Province")
                .Single(p => p.GetComponent<ProvinceController>().ProvinceData.Name.Equals(ProvinceData.Name))
                .GetComponent<ProvinceController>();
            
            switch (building)
            {
                case Constants.MineButtonIdentifier :
                    Debug.Log(province.ConstructBuilding(new Mine()));
                    break;
            }
        }
    }
}
