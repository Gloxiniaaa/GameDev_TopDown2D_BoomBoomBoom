using UnityEngine;

public class EnemyATK : MonoBehaviour
{
    protected Collider2D _collider;
    [SerializeField] private AudioGroupSO _sfx;

    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void ActivateCollider()
    {
        _sfxChannel.RaiseEvent(_sfx);
        _collider.enabled = true;
    }

    protected virtual void DeactivateColldier()
    {
        _collider.enabled = false;
    }

    protected virtual void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
