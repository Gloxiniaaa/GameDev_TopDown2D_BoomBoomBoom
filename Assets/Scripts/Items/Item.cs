using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemEffectSO _itemEffect;
    [SerializeField] private AudioGroupSO _collectSfx;

    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    private void OnEnable()
    {
        Float();
    }

    private void Float()
    {
        transform.position += Vector3.down * 0.1f;
        transform.DOLocalMove(transform.position + Vector3.up * 0.2f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.PlayerTag))
        {
            _sfxChannel.RaiseEvent(_collectSfx);
            _itemEffect.ApllyEffect();
            transform.DOKill();
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}