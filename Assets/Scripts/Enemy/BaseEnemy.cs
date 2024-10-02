using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IBombDamageable
{
    [SerializeField] protected AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;
    [SerializeField] protected VoidEventChannelSO _enemyDeathChannel;
    public Grid Grid { get; protected set; }
    [HideInInspector] public bool CanBeAttacked = true;
    public Rigidbody Rb {get; protected set;}
    [HideInInspector] public Animator Animator { get; protected set; }
    [HideInInspector] public Vector3 Direction;



    protected void Awake()
    {
        Grid = GameObject.Find("Grid").GetComponent<Grid>();
        Animator = GetComponent<Animator>();
    }

    public void TakeExplosionDamage(Vector3 pos)
    {
        Die();
    }

    private void Die()
    {
        _enemyDeathChannel.RaiseEvent();
        _sfxChannel.RaiseEvent(_enemyDieSfx);
        Destroy(gameObject);
    }
}