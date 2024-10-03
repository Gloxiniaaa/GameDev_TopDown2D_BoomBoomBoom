using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IBombDamageable
{
    public Grid Grid { get; protected set; }
    [HideInInspector] public bool CanBeAttacked = true;
    [HideInInspector] public Animator Animator { get; protected set; }
    [HideInInspector] public Vector3 Direction;
    protected EnemyState _state;


    protected void Awake()
    {
        Grid = GameObject.Find("Grid").GetComponent<Grid>();
        Animator = GetComponent<Animator>();
    }

    public virtual void TakeExplosionDamage(Vector3 pos)
    {
    }

    public void SwitchState(EnemyState newState)
    {
        _state.Exit();
        newState.Enter();
        _state = newState;
    }
}