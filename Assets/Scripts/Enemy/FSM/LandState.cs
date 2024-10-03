using UnityEngine;
using System.Collections;

public class LandState : EnemyState
{
    private static int _landTriggerHash = Animator.StringToHash("Attack");
    [SerializeField] private float _duration;

    public override void Do()
    {
    }

    public override void Enter()
    {

        _host.Animator.SetTrigger(_landTriggerHash);
        StartCoroutine(SelfExit());
    }

    private IEnumerator SelfExit()
    {
        yield return new WaitForSeconds(_duration);
        _host.SwitchState(_nextState);
    }

    public override bool Exit()
    {
        StopAllCoroutines();
        return true;
    }
}