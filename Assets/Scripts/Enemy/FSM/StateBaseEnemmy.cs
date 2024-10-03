using UnityEngine;

public class StateBaseEnemmy : BaseEnemy
{
    [SerializeField] private EnemyState _initialState;

    private void OnEnable()
    {
        _state = _initialState;
        _state.Enter();
    }

    private void Update()
    {
        _state.Do();
    }
}