using GlobalDatas;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.ProvinceMenu
{
    public class ProvinceMenuController : MonoBehaviour
    {
        public static ProvinceMenuController Instance { get; private set; }
        public ProvinceData ProvinceData;
        public RectTransform menuTransform;
        private bool _isHidden;
        private bool _buildingsPanelActive;
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
            _isHidden = true;
            _buildingsPanelActive = false;
            buildingsPanel.SetActive(_buildingsPanelActive);
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

        public void ShowBuildings()
        {
            _buildingsPanelActive = !_buildingsPanelActive;
            buildingsPanel.SetActive(_buildingsPanelActive);
        }
    }
}
