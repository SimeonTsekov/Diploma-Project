using System.Linq;
using Combat;
using Player;
using TMPro;
using UI.ArmyInfo;
using UI.ProvinceMenu;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void HideProvinceMenu()
        {
            ProvinceMenuController.Instance.Hide();
        }
        
        public void HideArmyMenu()
        {
            ArmyMenuController.Instance.Hide();
            var army = GameObject.FindGameObjectsWithTag("Army").Single(a =>
                a.GetComponent<ArmyController>().nationId.Equals(PlayerController.Instance.NationId));
            army.GetComponent<ArmyController>().selected = false;
        }
    }
}
