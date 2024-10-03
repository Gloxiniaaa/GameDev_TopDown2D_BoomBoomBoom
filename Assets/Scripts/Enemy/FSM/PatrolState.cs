using UnityEngine;

public class PatrolState : RandomMoveState
{
    [SerializeField] private float _detectRange;
    [SerializeField] private EnemyState _attackState;
    private RaycastHit2D playerDectector => Physics2D.CircleCast(_host.transform.position, _detectRange, Vector2.zero, 0f, 1 << Constant.PlayerLayer);

    public override void Do()
    {
        base.Do();
        if (playerDectector.collider)
        {
            _host.SwitchState(_attackState);
        }
    }
}