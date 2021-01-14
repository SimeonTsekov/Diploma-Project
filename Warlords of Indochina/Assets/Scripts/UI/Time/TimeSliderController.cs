using TimeControl;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Time
{
    public class TimeSliderController : MonoBehaviour
    {
        private Slider _slider;
        
        private void Awake()
        {
            _slider = gameObject.GetComponent<Slider>();
            _slider.value = Constants.MinSpeed - 1;
        }

        private void Update()
        {
            _slider.value = TimeController.Instance.speed - 1;
        }
    }
}
