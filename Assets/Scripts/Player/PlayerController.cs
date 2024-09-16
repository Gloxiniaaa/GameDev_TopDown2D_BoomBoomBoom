using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region input
    private PlayerControls _playerControls;
    private InputAction _move;
    [SerializeField] private LayerMask _solidLayer;
    #endregion 

    #region  animation
    private Animator _animator;
    private int _moveAnimId = Animator.StringToHash("move");
    private int _dirXHash = Animator.StringToHash("dirX");
    private int _dirYHash = Animator.StringToHash("dirY");
    #endregion

    #region attibute
    [SerializeField] private float _speed;
    private Vector3 _direction = Vector3.zero;
    #endregion

    #region grid placement
    [SerializeField] private Grid _mapGrid;
    [SerializeField] private GameObject _cellIndicator;
    private Vector3 _nextCell;
    private RaycastHit2D _obstacleDetector => Physics2D.Raycast(_nextCell, _direction, 0.1f, _solidLayer);
    #endregion

    #region boom
    [SerializeField] private GameObject _fireBall;
    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _move = _playerControls.Movement.Move;

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _move.Enable();
    }

    private void Update()
    {
        Move();
        IndicateCellPosition();
    }

    private void OnFire()
    {
        Debug.Log("install a boom");
        if (_fireBall)
        {
            GameObject fireBall = Instantiate(_fireBall, _cellIndicator.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), fireBall.GetComponent<Collider2D>());
        }
    }

    private void OnMove()
    {
        _animator.SetBool(_moveAnimId, true);
    }


    private void Move()
    {
        _direction = _move.ReadValue<Vector2>();
        _direction.Normalize();
        if (_direction != Vector3.zero)
        {
            _animator.SetFloat(_dirXHash, _direction.x);
            _animator.SetFloat(_dirYHash, _direction.y);
            if (IsNextCellAvailable())
                transform.position = Vector2.MoveTowards(transform.position, transform.position + _direction, 0.01f * _speed);
        }
        else
        {
            _animator.SetBool(_moveAnimId, false);
        }
    }

    private void IndicateCellPosition()
    {
        Vector3Int cellPos = _mapGrid.WorldToCell(transform.position);
        Vector3 offset = Vector2.one * 0.5f;
        _cellIndicator.transform.position = _mapGrid.CellToWorld(cellPos) + offset;
    }

    private bool IsNextCellAvailable()
    {
        Vector3Int nextCell = _mapGrid.WorldToCell(transform.position + _direction * 0.4f);
        Vector3 offset = Vector2.one * 0.5f;
        _nextCell = _mapGrid.CellToWorld(nextCell) + offset;
        Collider2D obstacle = _obstacleDetector.collider;
        if (obstacle)
        {
            return obstacle.transform.position == _cellIndicator.transform.position;
        }
        else
        {
            return true;
        }
    }

    private void OnDisable()
    {
        _move.Disable();
    }
}