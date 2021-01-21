using System;
using System.Collections.Generic;
using Economy;
using GlobalDatas;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class PlayerController : NationController
    {
        public static PlayerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            ResourceManagement = gameObject.AddComponent<ResourceManagemnt>();
            NationId = "";
        }

        public void SetNationId(string nationId)
        {
            this.NationId = nationId;
        }

        public void SubstractGold(int amount)
        {
            ResourceManagement.SubstractGold(amount);
        }
    }
}
