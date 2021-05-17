using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HideAndSeek
{
    public class LoadLevel : MonoBehaviour //кнопка загрузки уровня
    {
        public SceneAsset level;
        [SerializeField] private TMP_Text _levelText;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Load);
        }

        public void Initialize()
        {
            _levelText.text = level.name;
        }

        public void Load()
        {
            SceneManager.LoadScene(level.name);
        }

        public void OpenLevel() //кнопка становится интекрактивной, если уровень уже открыт
        {
            _button.interactable = true;
        }
    }
}
