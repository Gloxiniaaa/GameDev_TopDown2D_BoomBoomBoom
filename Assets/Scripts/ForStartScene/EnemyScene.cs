using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyScene : MonoBehaviour
{
    [SerializeField] private RectTransform _itself;
    private void OnEnable() {
        EnemyEffect();
    }
    
    
    private void EnemyEffect() {
        Sequence se = DOTween.Sequence();
        se.Append(_itself.DOScaleY(3.4f, 1f));
        se.Join(DOVirtual.DelayedCall(0.2f, () => {
            _itself.DOAnchorPos(new Vector2(73f, -152f), 0.8f);
        }));

        se.Append(_itself.DOScaleY(3.2f, 1f));
        se.Join(DOVirtual.DelayedCall(0.2f, () => {
            _itself.DOAnchorPos(new Vector2(79f, -141f), 0.8f);
        }));
       
        se.SetLoops(-1, LoopType.Yoyo);

    }
    private void OnDisable() {
        _itself.DOKill();
    }
}
