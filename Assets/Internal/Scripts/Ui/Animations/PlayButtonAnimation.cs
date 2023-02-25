using DG.Tweening;
using UnityEngine;

namespace Internal.Scripts.Ui.Animations
{
    public class PlayButtonAnimation : MonoBehaviour
    {
        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.Linear));
            _sequence.AppendInterval(2f);
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}