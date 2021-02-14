using System;
using System.Collections.Generic;
using Combat;
using Economy;
using GlobalDatas;
using Nations;
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
            ResourceManagement = gameObject.AddComponent<ResourceManagement>();
            CombatController = gameObject.AddComponent<CombatController>();
            NationId = "";
            atWar = new List<string>();
        }


    }
}
