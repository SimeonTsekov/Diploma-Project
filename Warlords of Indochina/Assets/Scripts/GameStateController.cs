using System.Linq;
using Player;
using Provinces;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        PlayerController.Instance.NationData = DbController.Instance.GetNationInfo(PlayerController.Instance.NationId);
        PlayerController.Instance.SetCapital();
        PlayerController.Instance.CreateArmy();
    }

}
