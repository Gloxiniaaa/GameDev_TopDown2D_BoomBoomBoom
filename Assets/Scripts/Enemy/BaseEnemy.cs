using UnityEngine;

public class BaseEnemy : MonoBehaviour, IBombDamageable
{
    [SerializeField] private AudioGroupSO _enemyDieSfx;
    [Header("Broadcast on channel:")]
    [SerializeField] protected AudioEventChannelSO _sfxChannel;
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;
    
    public void TakeExplosionDamage(Vector3 pos)
    {
    }
}