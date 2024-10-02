using UnityEngine;

public class Vomfy : BaseEnemy
{
    private EnemyState _state;
    [SerializeField] private RandomMoveState _randomMoveState;

    private void Start()
    {
        _state = _randomMoveState;
        _state.Enter();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(ChangeDirection), 1f, 3f);
    }

    private void Update()
    {
        _state.Do();
    }

    private void ChangeDirection()
    {
        SwitchState(_randomMoveState);
    }

    protected void SwitchState(EnemyState newState)
    {
        _state.Exit();
        newState.Enter();
        _state = newState;
    }

}