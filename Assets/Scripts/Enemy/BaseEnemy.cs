using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IBombDamageable
{
    [SerializeField] protected AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;
    [SerializeField] protected VoidEventChannelSO _enemyDeathChannel;
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

    public void TakeExplosionDamage(Vector3 pos)
    {
        if (CanBeAttacked)
            Die();
    }

    protected void Die()
    {
        _enemyDeathChannel.RaiseEvent();
        _sfxChannel.RaiseEvent(_enemyDieSfx);
        Destroy(gameObject);
    }

    public void SwitchState(EnemyState newState)
    {
        _state.Exit();
        newState.Enter();
        _state = newState;
    }
}