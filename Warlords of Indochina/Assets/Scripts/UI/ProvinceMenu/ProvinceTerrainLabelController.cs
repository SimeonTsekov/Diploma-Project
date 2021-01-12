using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProvinceTerrainLabelController : MonoBehaviour
{
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "Terrain: ";
    }

    void Update()
    {
        try
        {
            txt.text = "Terain: " + ProvinceMenuController.Instance.provinceData.terrain;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}