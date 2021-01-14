using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public string NationId { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            NationId = "";
        }

        public void SetNationId(string nationId)
        {
            this.NationId = nationId;
            Debug.Log(NationId);
        }
    }
}
