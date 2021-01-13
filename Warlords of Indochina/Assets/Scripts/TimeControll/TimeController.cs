using System;
using System.Globalization;
using UnityEngine;

namespace TimeControll
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }
        private DateTime _date;
        private float _timer;
        private int _delayAmount;
        private int _speed;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            _date = new DateTime(1444, 11, 11);
            _timer = 0f;
            _delayAmount = 1;
            _speed = 1;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
 
            if (_timer >= _delayAmount)
            {
                _timer = 0f;
                _date = _date.AddDays(1);
            }
        }

        public string GetDate()
        {
            return _date.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
        }

        public void OnPause()
        {
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.1f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }
    }
}
