using System.Collections;
using Database;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private RectTransform hand;
        [SerializeField] private TextMeshProUGUI coin;
        private const float Speed = 1f;
        private bool _moveRight = true;
        private void Start()
        {
            MoveHand();
            StartCoroutine(SetCoin());
        }

        private void MoveHand()
        {
            var targetX = _moveRight ? 150f : -150f;

            hand.DOAnchorPosX(targetX, Speed)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _moveRight = !_moveRight;
                    MoveHand();
                });
        }

        private IEnumerator SetCoin()
        {
            bool getCoin = false;
            while (!getCoin)
            {
                try
                {
                    coin.text = DatabaseManager.Instance.GetMoney().ToString();
                    getCoin = true;
                }
                catch
                {
                    // ignored
                }

                yield return null;
            }
        }
    }
}
