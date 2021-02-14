using System;
using System.Linq;
using Economy.Buildings;
using GlobalDatas;
using Player;
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
        public ProvinceController province;
        public RectTransform menuTransform;
        private bool _isHidden;
        public int currentSlot;
        private bool _buildingSlotsPanelActive;
        private bool _buildingsPanelActive;
        public GameObject buildingSlotsPanel;
        public GameObject buildingsPanel;
        public GameObject buildingsButton;
        public GameObject warButton;

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
            try
            {
                buildingsPanel.SetActive(_buildingsPanelActive);
                buildingSlotsPanel.SetActive(_buildingSlotsPanelActive);
            }
            catch (Exception) {}
        }

        public void UpdateProvinceData(ProvinceData provinceData)
        {
            this.ProvinceData = provinceData;
            province = GameObject.FindGameObjectsWithTag("Province")
                .Single(p => p.GetComponent<ProvinceController>().ProvinceData.Name.Equals(ProvinceData.Name))
                .GetComponent<ProvinceController>();
            UpdateBuildingsButtonState();
            UpdateDeclareWarButtonState();
            if (buildingSlotsPanel.activeSelf)
            {
                UpdateProvinceSlots();
            }
        }

        private void UpdateProvinceSlots()
        {
            for (var i=0; i<province.BuildingManagement.Buildings.Count; i++)
            {
                var slot = GameObject.FindGameObjectsWithTag("BuildingSlot")
                    .Single(s => int.Parse(s.name) == i+1);
                var building = province.BuildingManagement.Buildings[i];

                if (!building.Built)
                {
                    slot.GetComponent<Button>().interactable = true;
                    slot.GetComponentInChildren<Text>().text = "+";
                }
                else
                {
                    slot.GetComponentInChildren<Text>().text = building.Name;
                    slot.GetComponent<Button>().interactable = false;
                }
            }
        }

        private void UpdateBuildingsButtonState()
        {
            buildingsButton.SetActive(province.ProvinceData.NationId.Equals(
                PlayerController.Instance.NationId));
        }
        
        private void UpdateDeclareWarButtonState()
        {
            warButton.SetActive(!province.ProvinceData.NationId.Equals(
                PlayerController.Instance.NationId));
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
            UpdateProvinceSlots();
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

            switch (building)
            {
                case Constants.MineButtonIdentifier :
                    province.ConstructBuilding(new Mine(), currentSlot-1);
                    break;
                case  Constants.BarracksButtonIdentifier:
                    province.ConstructBuilding(new Barracks(), currentSlot-1);
                    break;
                case  Constants.FortButtonIdentifier:
                    province.ConstructBuilding(new Fort(), currentSlot-1);
                    break;
            }
            
            UpdateProvinceSlots();
        }

        public void DeclareWar()
        {
            PlayerController.Instance.DeclareWar(ProvinceData.NationId);
        }
    }
}
