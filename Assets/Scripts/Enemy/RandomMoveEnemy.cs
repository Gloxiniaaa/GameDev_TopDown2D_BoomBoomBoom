
using UnityEngine;

public class RandomMoveEnemy : Enemy
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layer;
    private Vector3 _dir;
    private Vector3 _nextPos;
    private int _dirXHash = Animator.StringToHash("MoveHori");
    private int _dirYHash = Animator.StringToHash("MoveVerti");

    private void Start()
    {
        Vector3Int curPos = _grid.WorldToCell(transform.position);

        int initDir = Random.Range(0, 4);
        if (initDir == 0) _dir = Vector3.down;
        else if (initDir == 1) _dir = Vector3.left;
        else if (initDir == 2) _dir = Vector3.right;
        else _dir = Vector3.up;

        _nextPos = _grid.CellToWorld(curPos) + _dir;
    }

    private void Update()
    {
        MoveAnim();
        Move(_nextPos);

    }

    private void MoveAnim()
    {
        _anim.SetFloat(_dirXHash, _rb.velocity.x);
        _anim.SetFloat(_dirYHash, _rb.velocity.y);
    }

    private void DetectNextStep()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir, 0.5f, _layer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(Constant.UntaggedTag) || hit.collider.CompareTag(Constant.DestroyableTag))
            {
                _dir = GetRandomDir(_dir);

            }
        }

        Vector3Int nextCell = _grid.WorldToCell(transform.position + _dir);
        _nextPos = _grid.GetCellCenterWorld(nextCell);
    }

    private void Move(Vector3 newPos)
    {
        _rb.velocity = (newPos - transform.position).normalized * _speed * Time.deltaTime;
        DetectNextStep();

    }

    private Vector3 GetRandomDir(Vector3 direct)
    {
        int ranNum = Random.Range(0, 3);
        if (direct == Vector3.down)
        {
            if (ranNum == 0) direct = Vector3.up;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.right;
        }
        else if (direct == Vector3.up)
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.right;
        }
        else if (direct == Vector3.right)
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.left;
            else direct = Vector3.up;
        }
        else
        {
            if (ranNum == 0) direct = Vector3.down;
            else if (ranNum == 1) direct = Vector3.up;
            else direct = Vector3.right;
        }
        return direct;
    }
}
