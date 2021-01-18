using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public class ManpowerLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "";
        }

        private void Update()
        {
            _txt.text = "Mnp: " + PlayerController.Instance.ResourceManagement.Manpower;
        }
    }
}
