using System;
using System.Collections.Generic;
using GlobalDatas;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public string NationId { get; private set; }
        public List<GameObject> Provinces;
        public int gold;
        public int manpower;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            NationId = "";
            gold = 100;
            manpower = 0;
        }

        public void SetNationId(string nationId)
        {
            this.NationId = nationId;
            Debug.Log(NationId);
        }

        public void SetProvinces(List<GameObject> provinceDatas)
        {
            this.Provinces = provinceDatas;
            this.manpower = provinceDatas.Count * Constants.StartingProvinceManpower;
        }
    }
}
