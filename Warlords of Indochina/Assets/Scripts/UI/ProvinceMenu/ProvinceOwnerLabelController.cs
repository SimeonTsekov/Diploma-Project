using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProvinceOwnerLabelController : MonoBehaviour
{
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "Owner: ";
    }

    void Update()
    {
        try
        {
            txt.text = "Owner: " + ProvinceMenuController.Instance.provinceData.owner;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
