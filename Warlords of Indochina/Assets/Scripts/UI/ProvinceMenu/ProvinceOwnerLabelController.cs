using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProvinceMenu
{
    public class ProvinceOwnerLabelController : MonoBehaviour
    {
        Text _txt;

        void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "Owner: ";
        }

        void Update()
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
