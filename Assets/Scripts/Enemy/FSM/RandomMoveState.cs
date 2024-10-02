using UnityEngine;

public class RandomMoveState : EnemyState
{
    [SerializeField] private float _speed;
    private Vector3 _nextCell;
    private RaycastHit2D _obstacleDetector => Physics2D.Raycast(_nextCell, _host.Direction, 0.1f, 1 << Constant.SolidLayer);
    private int _dirXHash = Animator.StringToHash("MoveHori");
    private int _dirYHash = Animator.StringToHash("MoveVerti");
    protected int _moveBoolHash = Animator.StringToHash("Move");

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
            Enter();
        }
    }

    public override void Enter()
    {
        _host.Direction = GetRandomDirection();
    }


    private bool IsNextCellAvailable()
    {
        _nextCell = GridExtensions.MapToGrid(_host.Grid, transform.position + _host.Direction * 0.51f);
        return _obstacleDetector.collider == null;
    }

    private Vector2 GetRandomDirection()
    {
        int i = Random.Range(0, 4);
        if (i == 0)
        {
            return Vector2.up;
        }
        else if (i == 1)
        {
            return Vector2.right;
        }
        else if (i == 2)
        {
            return Vector2.down;
        }
        else
        {
            return Vector2.left;
        }
    }

    public override bool Exit()
    {
        return true;
    }
}