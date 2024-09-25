using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyATK : MonoBehaviour
{
    protected CamShake _cam;
    protected virtual void OnEnable() {
        _cam = GameObject.FindGameObjectWithTag(Constant.CamTag).GetComponent<CamShake>();
    }
    protected virtual void SetInactive() {
        gameObject.SetActive(false);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(Constant.PlayerTag) && _cam != null) {
            _cam.Shake(0.1f, 0.5f);
        }
    }
}
