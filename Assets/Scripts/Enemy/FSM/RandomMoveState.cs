using UnityEngine;
using System.Collections;

public class RandomMoveState : EnemyState
{
    [SerializeField] protected float _speed;
    [SerializeField] protected int _duration;
    private Vector3 _nextCell;
    private RaycastHit2D _obstacleDetector => Physics2D.Raycast(_nextCell, _host.Direction, 0.1f, 1 << Constant.SolidLayer);
    private static int _dirXHash = Animator.StringToHash("MoveHori");
    private static int _dirYHash = Animator.StringToHash("MoveVerti");

    public override void Do()
    {
        if (IsNextCellAvailable())
        {
            _host.Animator.SetFloat(_dirXHash, _host.Direction.x);
            _host.Animator.SetFloat(_dirYHash, _host.Direction.y);
            _host.transform.position = Vector3.MoveTowards(_host.transform.position, _nextCell, _speed * Time.deltaTime);
        }
        else
        {
            _host.Direction = GridExtensions.GetRandomDirection();
        }
    }

    public override void Enter()
    {
        _host.Direction = GridExtensions.GetRandomDirection();
        StartCoroutine(SelfExit());
    }

    private IEnumerator SelfExit()
    {
        yield return new WaitForSeconds(_duration);
        _host.SwitchState(_nextState);
    }

    private bool IsNextCellAvailable()
    {
        _nextCell = GridExtensions.MapToGrid(_host.Grid, transform.position + _host.Direction * 0.51f);
        return _obstacleDetector.collider == null;
    }

    public override bool Exit()
    {
        StopAllCoroutines();
        return true;
    }
}