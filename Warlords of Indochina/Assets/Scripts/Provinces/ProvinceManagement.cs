using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDatas;
using System.Collections.ObjectModel;

public class ProvinceManagement : MonoBehaviour
{
    public static ProvinceManagement Instance { get; private set; }
    public List<ProvinceData> provinces { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        provinces = new List<ProvinceData>();
    }

    void Start()
    {
 
    }

    public void AddProvince(ProvinceData province)
    {
        provinces.Add(province);
    }
}
