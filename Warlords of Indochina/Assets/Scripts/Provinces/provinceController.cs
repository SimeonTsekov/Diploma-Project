using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDatas;
using System;

[RequireComponent(typeof(PolygonCollider2D))]

public class ProvinceController : MonoBehaviour
{
    SpriteRenderer sprite;
    public string gameObjectName;
    public ProvinceData provinceData;
    public Color color = new Color();

    void Start()
    {
        gameObjectName = gameObject.name;
        try
        {
            provinceData = ProvinceManagement.Instance.provinces.Find(x => Equals(x.name, gameObjectName));
            ColorUtility.TryParseHtmlString(provinceData.color, out color);
        }
        catch (Exception e) 
        {
            Debug.Log(e);
        }
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    void OnMouseEnter()
    {
        sprite.color = new Color(color.r, color.g, color.b, 0.75f);
    }

    void OnMouseExit()
    {
        sprite.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    void OnMouseDown()
    {
        ProvinceMenuController.Instance.UpdateProvinceData(this.provinceData);
    }
}
