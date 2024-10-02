using UnityEngine;

public class Vomfy : BaseEnemy
{
    [SerializeField] private EnemyState _initState;

    private void OnEnable()
    {
        _state = _initState;
        _state.Enter();
    }

    private void Update()
    {
        _state.Do();
    }
}