using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideAndSeek
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private List<EnemyController> _enemies; //список врагов
        [SerializeField] private Transform _startPoint; //точка появления игрока
        [SerializeField] private GameOver _gameOver;
        [SerializeField] private LoadScene _levelComplete;
        [SerializeField] private SceneAsset _nextLevel;

        private PlayerController _player;

        void Start()
        {
            Sequence seq = DOTween.Sequence();

            seq
                .AppendInterval(1)
                .AppendCallback(CreatePlayer) //через секунду создаётся игрок
                .AppendCallback(BeginSearch); //враги узнают какую цель ищут
        }

        private void CreatePlayer() //создание игрока
        {
            _player = Instantiate(_playerPrefab);
            _player.transform.position = _startPoint.position;
        }

        private void BeginSearch()
        {
            _player.CompleteLevel += Complete; //подписываемся на событие, когда игрок доходит до выхода

            foreach (var enemy in _enemies)
            {
                enemy.SetTarget();
                enemy.targetFound += GameOver; //подписываемся на событие, если игрок будет найден
            }
        }

        private void GameOver() //показывается интерфейс проигрыша
        {
            _player.enabled = false;
            _gameOver.Show();
        }

        private void Complete() //показывается интервейс победы
        {
            foreach (var enemy in _enemies)
            {
                enemy.targetFound -= GameOver; //отписываемся от события поиска игрока, альтернативно можно поставить игру на паузу
            }

            string levelName = SceneManager.GetActiveScene().name;
            SaveLoad.levelsCompleted = Convert.ToInt32(levelName.Substring(levelName.Length - 1));
            SaveLoad.Save();
            _player.enabled = false;
            _levelComplete.gameObject.SetActive(true);
            _levelComplete.nextLevel = _nextLevel;
        }
    }
}
