using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IBombDamageable destroyable =  other.gameObject.GetComponent<IBombDamageable>() as IBombDamageable;
        if (destroyable != null)
        {
            destroyable.TakeExplosionDamage(transform.position);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
