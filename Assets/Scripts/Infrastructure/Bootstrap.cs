using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}