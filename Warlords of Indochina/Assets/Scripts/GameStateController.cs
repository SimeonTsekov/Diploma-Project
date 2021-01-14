using System;
using System.Collections;
using System.Collections.Generic;
using Provinces;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        await ProvinceManagement.Instance.LoadProvinces();
    }

}
