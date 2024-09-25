using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private CamShake _cam;
    private void OnEnable()
    {
        _cam = GameObject.FindGameObjectWithTag(Constant.CamTag).GetComponent<CamShake>();
        Invoke(nameof(Deactivate), 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.BombTag))
        {
            // for chain explosion
            FireBall otherBomb = other.GetComponent<FireBall>();
            otherBomb.Explode();
        }
        if(other.CompareTag(Constant.PlayerTag) && _cam != null) {
            _cam.Shake(0.1f, 0.5f);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
