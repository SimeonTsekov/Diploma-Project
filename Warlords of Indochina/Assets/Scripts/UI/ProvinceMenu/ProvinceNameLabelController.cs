using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class ProvinceNameLabelController : MonoBehaviour
    {
        Text _txt;

        void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "";
        }

        void Update()
        {
            try
            {
                _txt.text = ProvinceMenuController.Instance.ProvinceData.Name;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
