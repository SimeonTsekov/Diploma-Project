using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class provinceController : MonoBehaviour
{
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    void OnMouseEnter()
    {
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    void OnMouseExit()
    {
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
