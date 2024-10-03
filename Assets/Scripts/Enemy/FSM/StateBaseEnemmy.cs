using UnityEngine;

public class StateBaseEnemmy : BaseEnemy
{
    [SerializeField] private EnemyState _initialState;
    [SerializeField] private EnemyState _deathState;

    private void OnEnable()
    {
        _state = _initialState;
        _state.Enter();
    }

    private void Update()
    {
        _state.Do();
    }

    public override void TakeExplosionDamage(Vector3 pos)
    {
        if (CanBeAttacked)
        {
            SwitchState(_deathState);
        }
    }
}