using System;
using System.Globalization;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace TimeControl
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }
        private DateTime _date;
        private float _timer;
        private float _delayAmount;
        public int speed;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            _date = new DateTime(1444, 11, 11);
            _timer = 0f;
            _delayAmount = 1.0f;
            speed = Constants.MinSpeed;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
 
            if (_timer >= _delayAmount)
            {
                _timer = 0f;
                _date = _date.AddDays(1);
                if (_date.Day == 1)
                {
                    PlayerController.Instance.ResourceManagement.UpdateResources();
                }
            }
        }

        public string GetDate()
        {
            return _date.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
        }

        public void OnPause()
        {
            Time.timeScale = Math.Abs(Time.timeScale - Constants.Pause) < 0.1f ? Constants.Unpause : Constants.Pause;
        }
        
        public void OnSpeedDown()
        {
            if (speed != Constants.MinSpeed)
            {
                speed--;
                _delayAmount += Constants.TimeStep;
            }
        }

        public void OnSpeedUp()
        {
            if (speed != Constants.MaxSpeed)
            {
                speed++;
                _delayAmount -= Constants.TimeStep;
            }
        }
    }
}
