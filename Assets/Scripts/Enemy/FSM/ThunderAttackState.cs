using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThunderAttackState : EnemyState
{
    private static int _attackTriggerHash = Animator.StringToHash("Attack");
    [SerializeField] private GameObject _preAttack;
    [SerializeField] private GameObject _thunder;
    [SerializeField] private int _numStrikes;
    private Transform _player;

    private Queue<GameObject> _thunderPool;

    private void Awake()
    {
        _preAttack.SetActive(false);
        _thunderPool = new Queue<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject thunder = Instantiate(_thunder);
            _thunderPool.Enqueue(thunder);
            thunder.SetActive(false);
        }
    }

    public override void Do()
    {
    }

    public override void Enter()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
            if (_player == null)
            {
                Debug.Log("cannot find player in current scene");
                return;
            }
        }
        _preAttack.SetActive(true);
        _host.Animator.SetTrigger(_attackTriggerHash);
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        for (int i = 0; i < _numStrikes; i++)
        {
            yield return new WaitForSeconds(1f);
            SpawnThunder();
        }
        _host.SwitchState(_nextState);
    }

    private void SpawnThunder()
    {
        GameObject thunderToSpawn = _thunderPool.Peek();
        if (thunderToSpawn.activeInHierarchy)
        {
            // means that all thunders are active, we have to add another one
            thunderToSpawn = Instantiate(_thunder);
            _thunderPool.Enqueue(thunderToSpawn);
        }
        else
        {
            thunderToSpawn = _thunderPool.Dequeue();
            _thunderPool.Enqueue(thunderToSpawn);
            thunderToSpawn.SetActive(true);
        }
        thunderToSpawn.transform.position = _player.transform.position;
    }

    public override bool Exit()
    {
        StopAllCoroutines();
        _preAttack.SetActive(false);
        return true;
    }
}