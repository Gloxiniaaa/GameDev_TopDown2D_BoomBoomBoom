using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IBombDamageable
{
    [SerializeField] protected AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;
    [SerializeField] protected VoidEventChannelSO _enemyDeathChannel;
    protected Grid _grid;

    protected void Awake()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    
    public void TakeExplosionDamage(Vector3 pos)
    {
    }
}