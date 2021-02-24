using System;
using System.Linq;
using Combat;
using GlobalDatas;
using Nations;
using Player;
using Provinces;
using UnityEngine;

namespace UI.ArmyInfo
{
    public class ArmyMenuController : MonoBehaviour
    {
        public static ArmyMenuController Instance { get; private set; }
        public RectTransform menuTransform;
        public ArmyController army;
        private bool _isHidden;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _isHidden = true;
        }
        
        
        public void Show()
        {
            if (!_isHidden) return;
            _isHidden = false;
            menuTransform.anchorMax = Vector2.zero;
            menuTransform.anchorMin = Vector2.zero;
        }

        public void SetArmyInformation(ArmyController armyController)
        {
            army = armyController;
        }

        public void OnBuildRegiment()
        {
            var nation = GameObject.FindGameObjectsWithTag("Nation")
                .Single(n => n.GetComponent<NationController>().NationData.NationId.Equals(army.nationId))
                .GetComponent<NationController>();
            
            StartCoroutine(GameStateController.Instance.BuildRegiment(army, nation));
        }
    }
}