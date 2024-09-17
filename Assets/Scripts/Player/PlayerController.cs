using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region input
    private PlayerControls _playerControls;
    private InputAction _move;
    #endregion 

    #region  animation
    private Animator _animator;
    private int _moveAnimId = Animator.StringToHash("move");
    private int _dirXHash = Animator.StringToHash("dirX");
    private int _dirYHash = Animator.StringToHash("dirY");
    #endregion

    #region attibute
    [SerializeField] private PlayerAttributeSO _playerAttribute;
    private Vector3 _direction = Vector3.zero;
    #endregion

    #region grid placement
    [SerializeField] private Grid _mapGrid;
    [SerializeField] private GameObject _cellIndicator;
    private Vector3 _nextCell;
    private RaycastHit2D _obstacleDetector => Physics2D.Raycast(_nextCell, _direction, 0.1f, 1 << Constant.SolidLayer);
    #endregion

    #region bomb
    [SerializeField] private BombBag _bombBag;
    #endregion

    [SerializeField] private AudioGroupSO _hurtSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] private AudioEventChannelSO _sfxChannel;


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
        if (_obstacleDetector.collider && _obstacleDetector.collider.CompareTag("Fireball"))
        {
            Debug.Log("this cell is already installed a boom");
            return;
        }
        _bombBag.InstallBomb(_cellIndicator.transform.position);
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
                transform.position += _direction * _playerAttribute.Speed * Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.ExplosionTag))
        {
            if (other.transform.position == _cellIndicator.transform.position)
            {
                _sfxChannel.RaiseEvent(_hurtSfx);
                Debug.Log("player is damaged by a bomb");
            }
        }
    }
    private void OnDisable()
    {
        _move.Disable();
    }
}