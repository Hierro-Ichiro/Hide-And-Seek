using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameOverText;
        [SerializeField] private Image _gameOverPanel;
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private float _fadeDuration = 1;

        public void Show()
        {
            gameObject.SetActive(true);

            Sequence seq = DOTween.Sequence();

            seq
                .Append(_gameOverText.DOFade(1, _fadeDuration)) //постепенное возникновение надписи
                .Join(_gameOverPanel.DOFade(1, _fadeDuration))
                .AppendCallback(() => _tryAgainButton.gameObject.SetActive(true)); //после появляется кнопка
        }
    }
}
