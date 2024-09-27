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
    private int _moveBoolHash = Animator.StringToHash("move");
    private int _dirXHash = Animator.StringToHash("dirX");
    private int _dirYHash = Animator.StringToHash("dirY");
    private int _hurtTriggerHash = Animator.StringToHash("hurt");
    private int _dieAnimHash = Animator.StringToHash("Die");
    #endregion

    #region attibute
    [SerializeField] private PlayerAttributeSO _playerAttribute;
    private Vector3 _direction = Vector3.zero;
    private bool _isDead = false;
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
    [SerializeField] private VoidEventChannelSO _camShakeChannel;
    [SerializeField] private IntEventChannelSO _playerGetsHurtChannel;
    [SerializeField] protected VoidEventChannelSO _playerDeathChannel;



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
        if (_isDead)
            return;

        RaycastHit2D currentCell = Physics2D.Raycast(_cellIndicator.transform.position, _direction, 0.1f, 1 << Constant.SolidLayer);
        if (currentCell.collider && currentCell.collider.CompareTag(Constant.BombTag))
        {
            Debug.Log("this cell is already installed a boom");
            return;
        }
        _bombBag.InstallBomb(_cellIndicator.transform.position);
    }

    private void OnMove()
    {
        _animator.SetBool(_moveBoolHash, true);
    }


    private void Move()
    {
        if (_move == null || _isDead)
            return;

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
            _animator.SetBool(_moveBoolHash, false);
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
                GetsHurt();
                Debug.Log("player is damaged by a bomb");
            }
        }
        else if (other.gameObject.layer == Constant.EnemyAtkLayer)
        {
            GetsHurt();
            Debug.Log("player is damaged by enemy ATK");
        }
    }

    private void GetsHurt()
    {
        _animator.SetTrigger(_hurtTriggerHash);
        _sfxChannel.RaiseEvent(_hurtSfx);
        _camShakeChannel.RaiseEvent();

        if (_playerAttribute.Health - 10 <= 0)
        {
            Die();
        }

        _playerGetsHurtChannel.RaiseEvent(10);
    }

    private void Die()
    {
        _isDead = true;
        _move.Disable();
        _animator.Play(_dieAnimHash);
        _playerDeathChannel.RaiseEvent();
    }

    private void OnDisable()
    {
        _move.Disable();
    }
}