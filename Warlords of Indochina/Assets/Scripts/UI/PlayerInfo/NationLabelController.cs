using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.PlayerInfo
{
    public class NationLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>();
            _txt.text = "";
        }

        private void Update()
        {
            _txt.text = NatioIdParser.ParseId(PlayerController.Instance.NationId);
        }
    }
}
