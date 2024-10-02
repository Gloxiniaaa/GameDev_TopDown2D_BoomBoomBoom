using UnityEngine;

public class Chomp : BaseEnemy
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