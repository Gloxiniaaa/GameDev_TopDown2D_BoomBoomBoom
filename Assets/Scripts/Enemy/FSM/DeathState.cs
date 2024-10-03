using UnityEngine;
using DG.Tweening;

public class DeathState : EnemyState
{
    [SerializeField] private AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;

    private static int _dieTriggerHash = Animator.StringToHash("GetDie");

    public override void Do()
    {
    }

    public override void Enter()
    {
        Die();
    }

    private void Die()
    {
        _host.CanBeAttacked = false;
        _host.Animator.SetTrigger(_dieTriggerHash);
        _sfxChannel.RaiseEvent(_enemyDieSfx);
        _host.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(1f).OnComplete(() => _host.gameObject.SetActive(false));
        _enemyDeathChannel.RaiseEvent();
    }

    public override bool Exit()
    {
        return true;
    }
}