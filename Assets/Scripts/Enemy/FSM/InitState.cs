using UnityEngine;
using System.Collections;

public class InitState : EnemyState
{
    public override void Do()
    {
    }

    public override void Enter()
    {
        _host.CanBeAttacked = false;
        StartCoroutine(SelfExit());
    }

    private IEnumerator SelfExit()
    {
        yield return new WaitForSeconds(Constant.EnemyInitTime);
        _host.SwitchState(_nextState);
        _host.CanBeAttacked = true;
    }

    public override bool Exit()
    {
        return true;
    }
}