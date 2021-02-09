using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using GlobalDatas;
using Nations;
using Player;
using Provinces;
using UnityEngine;


public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    public List<NationData> NationDatas { get; private set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        await ProvinceManagement.Instance.LoadProvinces();
        
        FetchNationDatas();
        SetPlayer();
        SetNations();
        SetArmies();
    }

    private void SetPlayer()
    {
        var player = PlayerController.Instance;
        player.ResourceManagement.SetProvinces(
            SetProvinces(PlayerController.Instance.NationId));

        player.NationData = NationDatas.Single(n => n.NationId.Equals(player.NationId));
        NationDatas.RemoveAll(n => n.NationId.Equals(player.NationId));
        player.SetCapital();
        player.CreateArmy();
    }

    private List<GameObject> SetProvinces(string NationId)
    {
        return GameObject.FindGameObjectsWithTag("Province")
                .Where(p => p.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(NationId))
                .ToList();
    }

    private void FetchNationDatas()
    {
       NationDatas = DbController.Instance.GetNationInfo();
    }

    private void SetNations()
    {
        foreach (var nation in NationDatas)
        {
            var newNation = new GameObject();
            var NationData = newNation.AddComponent<AIController>();
            NationData.SetNationData(nation);
            newNation.name = nation.Name + "AI";
            newNation.tag = "Nation";
            NationData.SetCapital();
            NationData.CreateArmy();
            NationData.SetNationId(nation.NationId);
        }
    }

    private void SetArmies()
    {
        foreach (var army in GameObject.FindGameObjectsWithTag("Army"))
        {
            army.GetComponent<ArmyController>().SetCurrentProvince();
        }
    }

    public IEnumerator Battle(GameObject army1, GameObject army2)
    {
        var armyController1 = army1.GetComponent<ArmyController>();
        var armyController2 = army2.GetComponent<ArmyController>();
        
        armyController1.SetFighting(true);
        armyController2.SetFighting(true);
        
        Debug.Log("FIGHT!!!");
        
        yield return null;
    }
}
