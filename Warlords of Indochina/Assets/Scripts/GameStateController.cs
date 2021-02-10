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
using Utils;


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
        var length = 1;
        var currentDefenderAdvantage = defenderController.CurrentProvince.GetComponent<ProvinceController>()
            .ProvinceData.DeffenderBonus;
        
        Debug.Log("FIGHT!!! " + defenderController.nationId + " " + attackerController.nationId);

        while (!Mathf.Approximately(defenderController.currentMorale, 0f) ||
               !Mathf.Approximately(attackerController.currentMorale, 0f))
        {
            while (!TimeController.Instance.Date.Equals(day.AddDays(1)))
            {
                yield return null;
            }

            day = TimeController.Instance.Date;

            var diceRollDefender = Mathf.Max(Random.Range(0, 9) + defenderController.strength - attackerController.strength + currentDefenderAdvantage + Constants.BaseDefenderAdvantage, 0);
            var diceRollAttacker = Mathf.Max(Random.Range(0, 9) + attackerController.strength - defenderController.strength - currentDefenderAdvantage, 0);

            Debug.Log(diceRollAttacker + " " + diceRollDefender);
            
            Debug.Log("Attacker cas: " + CalculateLosses(attackerController, diceRollDefender, length));

            if (attackerController.troops <= 0)
            {
                Destroy(attacker);
                break;
            }

            Debug.Log("Defender cas " + CalculateLosses(defenderController, diceRollAttacker, length));
            
            if (defenderController.troops <= 0)
            {
                Destroy(defender);
                break;
            }
            
            length++;
        }
        
        yield return null;
    }

    private int CalculateLosses(ArmyController armyController, int diceRoll, int length)
    {
        var baseCasualties = 15 + 5 * diceRoll;
        var casualties = baseCasualties + baseCasualties * (1 + length) / 100;

        armyController.troops -= casualties * 10;
        armyController.SetStrength();
        
        return casualties;
    }
}
