using System.Collections.Generic;
using GlobalDatas;
using UnityEngine;

namespace Provinces
{
    public class ProvinceManagement : MonoBehaviour
    {
        public static ProvinceManagement Instance { get; private set; }
        public List<ProvinceData> Provinces { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Provinces = new List<ProvinceData>();
        }

        public void AddProvince(ProvinceData province)
        {
            Provinces.Add(province);
        }
    }
}
