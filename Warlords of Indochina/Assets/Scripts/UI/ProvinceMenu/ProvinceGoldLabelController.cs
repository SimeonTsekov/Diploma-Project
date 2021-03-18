using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class ProvinceGoldLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "";
        }

        private void Update()
        {
            try
            {
                _txt.text = "Local Gold: " + ProvinceMenuController.Instance.province.ProvinceData.Gold.ToString("0.00");
            }
            catch (Exception)
            {
            }
        }
    }
}
