using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideAndSeek
{
    public class LoadScene : MonoBehaviour
    {
        public SceneAsset nextLevel;

        public void TryAgain() //запустить уровень заново
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextLevel() //перейти к следующему уровню
        {
            SceneManager.LoadScene(nextLevel.name);
        }

        public void Exit() //выйти в меню
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
