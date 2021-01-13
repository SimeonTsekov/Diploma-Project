using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalDatas;
using UnityEngine;

namespace Provinces
{
    public class ProvinceManagement : MonoBehaviour
    {
        public static ProvinceManagement Instance { get; private set; }
        public List<ProvinceData> Provinces { get; private set; }
        public List<IProvinceFetchedListener> ProvinceFetchedListeners;
        
        private async void Awake()
        {
            Debug.Log("Awake");
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            ProvinceFetchedListeners = new List<IProvinceFetchedListener>();
            Provinces = await GetProvinces();
            foreach (var x in ProvinceFetchedListeners)
            {
                x.OnProvincesFetched(Provinces);
            }
            Debug.Log("Ending Awake");
        }

        private async Task<List<ProvinceData>> GetProvinces()
        {
            return await DbController.Instance.GetProvinceInfo();
        }
    }
}
