using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDatas;

public class ProvinceMenuController : MonoBehaviour
{
    public static ProvinceMenuController Instance { get; private set; }
    public ProvinceData provinceData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateProvinceData(ProvinceData provinceData)
    {
        this.provinceData = provinceData;
    }
}
