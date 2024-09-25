using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : EnemyATK
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D _col;
    protected override void OnEnable() {
        _col.enabled = false;
        base.OnEnable();
    }
    private void Update() {
        if(_cam == null) Debug.Log("null");
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(Constant.PlayerTag) && _cam != null) {
            _cam.Shake(0.1f, 0.5f);
        }
    }
    private void DisplayColliderATK() {
        _col.enabled = true;
    }
    private void InactiveColliderATK() {
        _col.enabled = false;
    }
    protected override void SetInactive()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
