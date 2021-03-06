﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class ProvinceManpowerLabelController : MonoBehaviour
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
                _txt.text = "Local Manpower: " + ProvinceMenuController.Instance.province.ProvinceData.Manpower;
            }
            catch (Exception)
            {
            }
        }
    }
}
