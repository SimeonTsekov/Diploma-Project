using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
