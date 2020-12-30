using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalDatas
{
    public class ProvinceData
    {
        public string name;
        public int buildingSlots;
        public int availableBuildingSlots;
        public string color;
        public string terrain;
        public int attrition;
        public int deffenderBonus;

        public ProvinceData(string name
            , int buildingSlots
            , int availableBuildingSlots
            , string color
            , string terrain
            , int attrition
            , int deffenderBonus)
        {
            this.name = name;
            this.buildingSlots = buildingSlots;
            this.availableBuildingSlots = availableBuildingSlots;
            this.color = color;
            this.terrain = terrain;
            this.attrition = attrition;
            this.deffenderBonus = deffenderBonus;
        }
    }
}