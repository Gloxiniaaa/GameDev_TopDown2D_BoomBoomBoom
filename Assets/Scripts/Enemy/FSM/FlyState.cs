using System.Collections;
using UnityEngine;

public class FlyState : EnemyState
{
    [SerializeField] private AudioGroupSO _flySfx;
    [SerializeField] private AudioEventChannelSO _sfxChannel;
    [SerializeField] private float _speed;
    [Tooltip("For easily mapping, we use int instead of float")]
    [SerializeField] private int _duration;

    private Vector3 _nextCell;
    private RaycastHit2D _obstacleDetector => Physics2D.Raycast(_nextCell, _host.Direction, 0.1f, 1 << Constant.SolidLayer);
    private static int _dirXHash = Animator.StringToHash("MoveHori");
    private static int _dirYHash = Animator.StringToHash("MoveVerti");
    private static int _flyTriggerHash = Animator.StringToHash("Fly");

    public override void Enter()
    {
        _host.CanBeAttacked = false;
        _sfxChannel.RaiseEvent(_flySfx);
        _host.Animator.SetTrigger(_flyTriggerHash);
        _host.Direction = GridExtensions.GetRandomDirection();
        StartCoroutine(EndFlyingAfterSeconds(_duration));
    }

    private IEnumerator EndFlyingAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        RaycastHit2D thisCellObstacle = Physics2D.Raycast(_host.transform.position, Vector2.one, 0.1f, 1 << Constant.SolidLayer);
        if (thisCellObstacle.collider)
        {
            // if curent cell has an obstacle, continue flying for 1 more second
            StartCoroutine(EndFlyingAfterSeconds(1));
        }
        else
        {
            _host.SwitchState(_nextState);
        }
    }

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

    private bool IsNextCellAvailable()
    {
        _nextCell = GridExtensions.MapToGrid(_host.Grid, transform.position + _host.Direction * 0.51f);
        if (_obstacleDetector.collider)
        {
            return _obstacleDetector.collider.CompareTag(Constant.DestroyableTag);
        }
        else
        {
            return true;
        }
    }

    public override bool Exit()
    {
        StopAllCoroutines();
        _host.CanBeAttacked = true;
        return true;
    }
}