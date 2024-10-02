using UnityEngine;

/// <summary>
/// saying damageable means that the object can be damaged by an explosion (bomb)
/// </summary>
public interface IBombDamageable
{
    public void TakeExplosionDamage(Vector3 pos);
}