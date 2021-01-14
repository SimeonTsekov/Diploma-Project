using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class StartButtonController : MonoBehaviour
	{
		public void StartGame()
		{
			if (!PlayerController.Instance.NationId.Equals(""))
			{
				SceneManager.LoadScene("MainGameScene");
			}
		}
	}
}