using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            _txt.text = "";
        }

        private void Update()
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            try
            {
                if (SceneManager.GetActiveScene().name.Equals("NationSelectionScene"))
                {
                    _txt.text = "Nation: " + NatioIdParser.ParseId(PlayerController.Instance.NationId);
                } else if (SceneManager.GetActiveScene().name.Equals("MainGameScene"))
                {
                    _txt.text = "Owner: " + NatioIdParser.ParseId(ProvinceMenuController.Instance.ProvinceData.Owner);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
