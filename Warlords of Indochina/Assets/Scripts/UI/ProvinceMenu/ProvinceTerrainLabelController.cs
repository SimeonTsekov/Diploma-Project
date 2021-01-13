using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class ProvinceTerrainLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "Terrain: ";
        }

        private void Update()
        {
            try
            {
                _txt.text = "Terrain: " + ProvinceMenuController.Instance.ProvinceData.Terrain;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}