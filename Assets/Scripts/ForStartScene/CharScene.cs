
using UnityEngine;
using DG.Tweening;
public class CharScene : MonoBehaviour
{
    [SerializeField] private RectTransform _itself;

    private void OnEnable()
    {
        CharEffect();
    }

    private void CharEffect()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_itself.DOAnchorPosY(155f, 1f));
        sequence.Append(_itself.DOAnchorPosY(140f, 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);

        _itself.DOAnchorPosY(155f, 1f).OnComplete(() => _itself.DOAnchorPosY(140f, 1f));
    }

    private void OnDisable()
    {
        _itself.DOKill();
    }
}
