using System.Collections;
using System.Collections.Generic;
using UI.PlayerInfo;
using UnityEngine;

namespace GlobalDatas
{
    public class ProvinceData
    {
        public string Name;
        public int BuildingSlots;
        public int AvailableBuildingSlots;
        public string Color;
        public string NationId;
        public string Owner;
        public string Terrain;
        public int Attrition;
        public int DeffenderBonus;
        public float Gold;
        public int Manpower;

        public ProvinceData(string name
            , int buildingSlots
            , int availableBuildingSlots
            , string color
            , string nationId
            , string owner
            , string terrain
            , int attrition
            , int deffenderBonus
            , float gold
            , int manpower)
        {
            this.Name = name;
            this.BuildingSlots = buildingSlots;
            this.AvailableBuildingSlots = availableBuildingSlots;
            this.Color = color;
            this.NationId = nationId;
            this.Owner = owner;
            this.Terrain = terrain;
            this.Attrition = attrition;
            this.DeffenderBonus = deffenderBonus;
            this.Gold = gold;
            this.Manpower = manpower;
        }
    }
}