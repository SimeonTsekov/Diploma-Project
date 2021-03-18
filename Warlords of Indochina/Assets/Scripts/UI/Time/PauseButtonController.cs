using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Time
{
    public class PauseButtonController : MonoBehaviour
    {
        private Text _txt; 
        private void Awake()
        {
            _txt = gameObject.GetComponent<Text>();
        }

        private void Update()
        { 
            _txt.text = Math.Abs(UnityEngine.Time.timeScale - Constants.Pause) < 0.1f ? ">" : "||";
        }
    }
}
