using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalDatas;
using UnityEngine;

namespace Provinces
{
    public class ProvinceManagement : MonoBehaviour
    {
        public static ProvinceManagement Instance { get; private set; }
        public List<IProvinceFetchedListener> ProvinceFetchedListeners;
        
        private async void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            await LoadProvinces();
        }

        private async Task<List<ProvinceData>> GetProvinces()
        {
            return await DbController.Instance.GetProvinceInfo();
        }

        public async Task LoadProvinces()
        {
            ProvinceFetchedListeners = new List<IProvinceFetchedListener>();
            var provinces = await GetProvinces();
            foreach (var x in ProvinceFetchedListeners)
            {
                x.OnProvincesFetched(provinces);
            }
        }
    }
}
