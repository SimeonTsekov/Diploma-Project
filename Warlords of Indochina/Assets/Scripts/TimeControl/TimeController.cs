﻿using System;
using System.Globalization;
using System.Linq;
using Combat;
using Nations;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace TimeControl
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }
        public DateTime Date { get; private set; }
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
            Date = new DateTime(1444, 11, 11);
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
                Date = Date.AddDays(1);
                    
                if (Date.Day == 1)
                {
                    var armies = GameObject.FindGameObjectsWithTag("Army")
                        .ToList();

                    foreach (var army in armies)
                    {
                        army.GetComponent<ArmyController>().RestoreMonthlyMorale();
                    }

                    var nations = GameObject.FindGameObjectsWithTag("Nation")
                        .ToList();

                    foreach (var nation in nations)
                    {
                        var nationController = nation.GetComponent<NationController>();
                        nationController.ResourceManagement.UpdateResources();
                        nationController.RefillArmy();
                    }
                }
            }
        }

        public string GetDateAsString()
        {
            return Date.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
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
