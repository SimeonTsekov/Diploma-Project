using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using GlobalDatas;
using Nations;
using Player;
using Provinces;
using TimeControl;
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

    public IEnumerator Battle(GameObject defender, GameObject attacker)
    {
        var defenderController = defender.GetComponent<ArmyController>();
        var attackerController = attacker.GetComponent<ArmyController>();
        
        defenderController.SetFighting(true);
        attackerController.SetFighting(true);

        var day = TimeController.Instance.Date;
        var currentDefenderAdvantage = defenderController.CurrentProvince.GetComponent<ProvinceController>()
            .ProvinceData.DeffenderBonus;
        
        Debug.Log("FIGHT!!! " + defenderController.NationId + " " + attackerController.NationId);

        while (!Mathf.Approximately(defenderController.CurrentMorale, 0f) ||
               !Mathf.Approximately(attackerController.CurrentMorale, 0f))
        {
            while (!TimeController.Instance.Date.Equals(day.AddDays(1)))
            {
                yield return null;
            }

            day = TimeController.Instance.Date;

            var diceRollDeffender = Random.Range(0, 9) + defenderController.Strength - attackerController.Strength + currentDefenderAdvantage;
            var diceRollAttacker = Random.Range(0, 9) + attackerController.Strength - defenderController.Strength - currentDefenderAdvantage;
            
            Debug.Log("Attacker - " + diceRollAttacker + "; Defender - " + diceRollDeffender);
        }
        
        yield return null;
    }
}
