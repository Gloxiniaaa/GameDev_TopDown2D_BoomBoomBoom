using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.FireBallTag))
        {
            // for chain explosion
            FireBall otherBomb = other.GetComponent<FireBall>();
            otherBomb.Explode();
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
