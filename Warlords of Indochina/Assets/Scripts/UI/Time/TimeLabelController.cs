using TimeControl;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Time
{
    public class TimeLabelController : MonoBehaviour
    {
        private Text _txt;

        private void Start()
        {
            _txt = GetComponent<Text>(); 
            _txt.text = TimeController.Instance.GetDateAsString();
        }

        private void Update()
        {
            _txt.text = TimeController.Instance.GetDateAsString();
        }
    }
}
