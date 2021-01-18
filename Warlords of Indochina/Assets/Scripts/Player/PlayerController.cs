using System;
using System.Collections.Generic;
using Economy;
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
        public ResourceManagemnt ResourceManagement { get; private set; }

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
    }
}
