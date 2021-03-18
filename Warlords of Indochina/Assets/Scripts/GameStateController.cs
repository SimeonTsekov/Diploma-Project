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
            newNation.name = nation.NationId + "AI";
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
        var baseCasualties = Constants.BaseCasualties + Constants.DiceAmplifier * diceRoll;
        var casualties = baseCasualties + baseCasualties * (1 + length) / Constants.CasualtiesDivisor;

        armyController.troops -= casualties * Constants.TroopsCasualtiesAmplifier;
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
        return casualties/Constants.MoraleLossDivisor 
            * (armyController.maximumMorale/Constants.MaxMoraleLossDivisor) 
            + Constants.DailyMoraleLoss;
    }

    public IEnumerator Siege(ArmyController army, ProvinceController province)
    {
        army.besieging = true;
        var siegeProgress = 0;
        var day = TimeController.Instance.Date;

        while (siegeProgress != Constants.ProvinceSiegeDuration)
        {
            if (!army.besieging)
            {
                yield break;
            }
            
            while (!TimeController.Instance.Date.Equals(day.AddDays(1)))
            {
                yield return null;
            }

            day = TimeController.Instance.Date;
            
            if (!army.fighting)
            {
                siegeProgress++;
            }
        }
        
        province.TransferProvince(army.nationId);

        army.besieging = false;
        yield return null;
    }

    public IEnumerator BuildRegiment(ArmyController army, NationController nation)
    {
        var day = TimeController.Instance.Date;

        if (nation.ResourceManagement.Manpower < Constants.RegimentTroops
            || nation.ResourceManagement.Gold < Constants.RegimentCost)
        {
            yield break;
        }
        
        nation.ResourceManagement.Manpower -= Constants.RegimentTroops;
        nation.ResourceManagement.Gold -= Constants.RegimentCost;
        
        while (!TimeController.Instance.Date.Equals(day.AddDays(Constants.RegimentBuildTime)))
        {
            yield return null;
        }

        army.Regiments++;
        army.troops += Constants.RegimentTroops;
        army.RestoreStrength();
    }
}
