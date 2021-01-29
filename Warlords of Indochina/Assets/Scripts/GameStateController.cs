using System.Linq;
using Player;
using Provinces;
using UnityEngine;


public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        await ProvinceManagement.Instance.LoadProvinces();
        PlayerController.Instance.ResourceManagement.SetProvinces(
            GameObject.FindGameObjectsWithTag("Province")
                .Where(p=> p.GetComponent<ProvinceController>().ProvinceData.NationId.Equals(PlayerController.Instance.NationId))
                .ToList());
    }

}
