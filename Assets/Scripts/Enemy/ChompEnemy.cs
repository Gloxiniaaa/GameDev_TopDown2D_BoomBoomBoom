using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChompEnemy : EnemyBase
{
    private int _attackX = Animator.StringToHash("AtkHori");
    private int _attackY = Animator.StringToHash("AtkVerti");
    private int _idleX = Animator.StringToHash("IdleHori");
    private int _idleY = Animator.StringToHash("IdleVerti");
    private int _fly = Animator.StringToHash("Fly");
    private int _atkTrigger = Animator.StringToHash("Attack");
    [SerializeField] private float _timeAttack;
    [SerializeField] private GameObject _smokeEffect;
    [SerializeField] private AudioGroupSO _flySfx;
    private bool _isAttacking = false;
    private bool _isFlying = false;


    protected override void MoveAnim()
    {
        if (_isFlying)
        {
            base.MoveAnim();
        }
    }
    private void OnEnable()
    {
        _smokeEffect.SetActive(false);
        InvokeRepeating(nameof(AttackCounter), 1, _timeAttack);
    }

    protected override void Move(Vector3 newPos)
    {
        if (!_isFlying)
        {
            float dis = Vector2.Distance(transform.position, _nextPos);
            if ((dis <= 1.1f && dis >= 0.9f) && !_isAttacking)
            {
                AttackFunct();
            }
            else
            {
                base.Move(newPos);
            }
        }
        else
        {
            base.Move(newPos);
        }
    }

    private void AttackCounter()
    {
        if (_isFlying)
        {
            _isFlying = false;
        }
    }
    private void AttackFunct()
    {
        _isAttacking = true;
        float moveXDir = _rb.velocity.x;
        float moveYDir = _rb.velocity.y;
        _anim.SetTrigger(_atkTrigger);
        _anim.SetFloat(_attackX, moveXDir);
        _anim.SetFloat(_attackY, moveYDir);
        _rb.velocity = Vector3.zero;
        StartCoroutine(WaitToFly(moveXDir, moveYDir));
    }
    private IEnumerator WaitToFly(float x, float y)
    {
        Debug.Log("Idle");
        _checkDie = true;
        _anim.SetFloat(_idleX, x);
        _anim.SetFloat(_idleY, y);
        yield return new WaitForSeconds(4f);

        _checkDie = false;
        _isAttacking = false;
        _anim.SetTrigger(_fly);
        _sfxChannel.RaiseEvent(_flySfx);
        _isFlying = true;
    }
    private void ActiveSmokeEffect()
    {
        _smokeEffect.SetActive(true);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isFlying)
        {
            if (other.gameObject.CompareTag(Constant.ExplosionTag))
            {
                _rb.velocity = Vector3.zero;
                DieEffect(other.transform.position);
            }
        }
    }
}
