using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProvinceNameLabelController : MonoBehaviour
{
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "";
    }

    void Update()
    {
        try
        {
            txt.text = ProvinceMenuController.Instance.provinceData.name;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
