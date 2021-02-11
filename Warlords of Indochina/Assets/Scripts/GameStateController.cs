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
            GetProvinces(PlayerController.Instance.NationId));

        player.NationData = NationDatas.Single(n => n.NationId.Equals(player.NationId));
        NationDatas.RemoveAll(n => n.NationId.Equals(player.NationId));
        player.SetCapital();
        player.CreateArmy();
    }

    private List<GameObject> GetProvinces(string NationId)
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
            var nationDataComponent = newNation.AddComponent<AIController>();
            nationDataComponent.SetNationData(nation);
            newNation.name = nation.Name + "AI";
            newNation.tag = "Nation";
            nationDataComponent.ResourceManagement.SetProvinces(GetProvinces(nationDataComponent.NationData.NationId));
            nationDataComponent.SetCapital();
            nationDataComponent.CreateArmy();
            nationDataComponent.SetNationId(nation.NationId);
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

        while (defenderController.currentMorale > 0 && attackerController.currentMorale > 0)
        {
            while (!TimeController.Instance.Date.Equals(day.AddDays(1)))
            {
                yield return null;
            }

            day = TimeController.Instance.Date;

            var diceRollDefender = Mathf.Max(Random.Range(0, 9) + defenderController.strength - attackerController.strength + currentDefenderAdvantage + Constants.BaseDefenderAdvantage, 0);
            var diceRollAttacker = Mathf.Max(Random.Range(0, 9) + attackerController.strength - defenderController.strength - currentDefenderAdvantage, 0);

            Debug.Log(attackerController.currentMorale + " " + defenderController.currentMorale);
            
            CalculateLosses(attackerController, diceRollDefender, length);

            if (attackerController.troops <= 0)
            {
                defenderController.ResetMorale();
                Destroy(attacker);
                break;
            }

            if (attackerController.currentMorale <= 0)
            {
                if (defenderController.troops / 2 > attackerController.troops)
                {
                    Destroy(attacker);
                }
                
                defenderController.ResetMorale();
                break;
            }

            CalculateLosses(defenderController, diceRollAttacker, length);
            
            if (defenderController.troops <= 0)
            {
                attackerController.ResetMorale();
                Destroy(defender);
                break;
            }
            
            if (defenderController.currentMorale <= 0)
            {
                if (attackerController.troops / 2 > defenderController.troops)
                {
                    Destroy(defender);
                }
                attackerController.ResetMorale();
                break;
            }
            
            length++;
        }
        
        defenderController.SetFighting(false);
        attackerController.SetFighting(false);
        
        yield return null;
    }

    private int CalculateLosses(ArmyController armyController, int diceRoll, int length)
    {
        var baseCasualties = 15 + 5 * diceRoll;
        var casualties = baseCasualties + baseCasualties * (1 + length) / 100;

        armyController.troops -= casualties * 10;
        armyController.SetStrength();
        armyController.currentMorale -= CalculateMoraleLosses(armyController, casualties);

        if (armyController.currentMorale <= 0)
        {
            armyController.currentMorale = 0f;
            Debug.Log(armyController.nationId + " lost");
            armyController.Retreat();
        }
            
        return casualties;
    }

    private float CalculateMoraleLosses(ArmyController armyController, float casualties)
    {
        return casualties/Constants.MoraleLossDivisor * (armyController.maximumMorale/Constants.MaxMoraleLossDivisor) + Constants.DailyMoraleLoss;
    }
}
