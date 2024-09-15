using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region input
    private PlayerControls _playerControls;
    private InputAction _move;
    private InputAction _installBoom;
    #endregion 


    #region  animation
    private Animator _animator;
    private int _moveAnimId = Animator.StringToHash("move");
    private int _dirXHash = Animator.StringToHash("dirX");
    private int _dirYHash = Animator.StringToHash("dirY");

    #endregion


    #region attibute
    [SerializeField] private float _speed;
    private Vector3 _direction;
    #endregion



    private void Awake()
    {
        _playerControls = new PlayerControls();
        _move = _playerControls.Movement.Move;
        _installBoom = _playerControls.Movement.Fire;

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _move.Enable();
        _installBoom.Enable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _direction = _move.ReadValue<Vector2>();
        _direction.Normalize();
        if (_direction != Vector3.zero)
        {
            _animator.SetFloat(_dirXHash, _direction.x);
            _animator.SetFloat(_dirYHash, _direction.y);
            _animator.SetBool(_moveAnimId, true);
            transform.position = Vector2.MoveTowards(transform.position, transform.position + _direction, 0.01f * _speed);
        }
        else
        {
            _animator.SetBool(_moveAnimId, false);
        }
    }

    private void OnDisable()
    {
        _move.Disable();
        _installBoom.Disable();
    }

}