using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JorgetEnemy : EnemyBase
{
    [SerializeField] private List<GameObject> _atkList;
    [SerializeField] private Vector3 _atkRange;
    [SerializeField] private float _timeAttack;
    [SerializeField] private GameObject _preAtk;
    private int _attackX = Animator.StringToHash("AtkHori");
    private int _attackY = Animator.StringToHash("AtkVerti");
    private int _atkTrigger = Animator.StringToHash("Attack");
    private bool _isAttacking = false;
    private bool _isStop = false;
    private GameObject _player;
    private void DetectPlayer()
    {
        _player = GameObject.Find("Player");
    }
    private IEnumerator Attack()
    {

        float moveXDir = _rb.velocity.x;
        float moveYDir = _rb.velocity.y;
        _anim.SetTrigger(_atkTrigger);
        _anim.SetFloat(_attackX, moveXDir);
        _anim.SetFloat(_attackY, moveYDir);
        _isStop = true;
        _rb.velocity = Vector3.zero;
        foreach (var atk in _atkList)
        {
            Vector3Int newPlayerPos = _grid.WorldToCell(_player.transform.position);
            atk.transform.position = _grid.GetCellCenterWorld(newPlayerPos);
            atk.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        _isStop = false;

    }
    private void OnEnable()
    {
        DetectPlayer();
        _preAtk.SetActive(false);
        foreach (var atk in _atkList)
        {
            atk.SetActive(false);
        }
        InvokeRepeating(nameof(AttackCounter), 1, _timeAttack);
    }
    protected override void Update()
    {
        if (!_isStop)
        {
            base.Update();
        }
    }
    private void AttackCounter()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
        }
    }
    protected override void MoveAnim()
    {
        if (!_isStop)
        {
            base.MoveAnim();
        }
    }
    protected override void Move(Vector3 newPos)
    {
        if (_isAttacking)
        {
            float dis = Vector2.Distance(transform.position, _nextPos);
            if (dis <= 1.1f && dis >= 0.9f && _player != null)
            {
                Vector3 atkDis = _player.transform.position - transform.position;
                if (Mathf.Abs(atkDis.x) < _atkRange.x && Mathf.Abs(atkDis.y) < _atkRange.y)
                {
                    _isAttacking = false;
                    StartCoroutine(Attack());
                    
                }
                else base.Move(newPos);
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
    private void DisplayPreATK()
    {
        _preAtk.SetActive(true);
    }
}
