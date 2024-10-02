using UnityEngine;

/// <summary>
/// single-transition base state for enemy finite state machine 
/// </summary>
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] protected BaseEnemy _host;
    [SerializeField] protected EnemyState _nextState;
    public abstract void Enter();
    public abstract void Do();
    public abstract bool Exit();
}