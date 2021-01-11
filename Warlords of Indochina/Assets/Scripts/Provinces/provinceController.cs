using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDatas;
using System;

[RequireComponent(typeof(PolygonCollider2D))]

public class provinceController : MonoBehaviour
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
            Debug.Log(provinceData.name);
        }
        catch (Exception e) { }
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    void OnMouseEnter()
    {
        sprite.color = new Color(color.r, color.g, color.b, 1f);
    }

    void OnMouseExit()
    {
        sprite.color = new Color(color.r, color.g, color.b, 0.5f);
    }
}
