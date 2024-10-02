using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] protected BaseEnemy _host;
    public bool IsComplete {get; protected set;}
    public abstract void Enter();
    public abstract void Do();
    public abstract bool Exit();
}