using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HideAndSeek
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private List<SceneAsset> _levels;
        [SerializeField] private LoadLevel _levelIconPrefab;
        [SerializeField] private GameObject _allLevels;
        [SerializeField] private Transform _example;

        private List<LoadLevel> _levelIcons;

        private void Start()
        {
            SaveLoad.Load(); //загрузка данных

            _levelIcons = new List<LoadLevel>();

            foreach (var level in _levels) //создание кнопок уровней
            {
                LoadLevel newIcon = Instantiate(_levelIconPrefab);
                newIcon.level = level;
                newIcon.Initialize();
                newIcon.transform.SetParent(_allLevels.transform);
                newIcon.transform.localScale = _example.localScale;
                newIcon.transform.position = _example.position;
                _levelIcons.Add(newIcon);
            }

            for (int i = 0; i <= SaveLoad.levelsCompleted && i < _levelIcons.Count; i++) //кнопки становятся кликабельными, если уровень уже открыт
            {
                _levelIcons[i].OpenLevel();
            }
        }
    }
}
