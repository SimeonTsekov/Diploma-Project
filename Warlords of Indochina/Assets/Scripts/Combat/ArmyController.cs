using UnityEngine;

namespace Combat
{
    public class ArmyController : MonoBehaviour
    {
        public string NationId { get; private set; }
        public int Troops { get; private set; }

        private void Awake()
        {
            NationId = "";
            Troops = 0;
        }

        public void InitializeArmy(string nationId, int troops)
        {
            NationId = nationId;
            Troops = troops;
        }
    }
}
