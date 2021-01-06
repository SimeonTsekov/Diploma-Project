using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDatas;
using System.Collections.ObjectModel;

public class ProvinceManagement : MonoBehaviour
{
    public static ProvinceManagement Instance { get; private set; }
    public List<ProvinceData> provinces { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        provinces = new List<ProvinceData>();
    }

    public void AddProvince(ProvinceData province)
    {
        provinces.Add(province);
    }
}
