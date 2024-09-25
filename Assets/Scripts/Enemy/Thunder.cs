using UnityEngine;

public class Thunder : EnemyATK
{
    [SerializeField] private Collider2D _col;
    [SerializeField] private AudioGroupSO _thunderSfx;

    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;

    protected override void OnEnable() {
        _col.enabled = false;
        base.OnEnable();
    }

    private void DisplayColliderATK() {
        _sfxChannel.RaiseEvent(_thunderSfx);
        _col.enabled = true;
    }
    
    private void InactiveColliderATK() {
        _col.enabled = false;
    }

    protected override void SetInactive()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
