using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.BombTag))
        {
            // for chain explosion
            Bomb otherBomb = other.GetComponent<Bomb>();
            otherBomb.Explode();
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
