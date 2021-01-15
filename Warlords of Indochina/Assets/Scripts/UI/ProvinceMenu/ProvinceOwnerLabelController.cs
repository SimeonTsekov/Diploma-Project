using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.ProvinceMenu
{
    public class ProvinceOwnerLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "Owner: ";
        }

        private void Update()
        {
            try
            {
                _txt.text = "Owner: " + ProvinceMenuController.Instance.ProvinceData.Owner;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
