using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public class GoldLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "";
        }

        private void Update()
        {
            _txt.text = "Gold: " + PlayerController.Instance.ResourceManagement.Gold.ToString("0.00");
        }
    }
}
