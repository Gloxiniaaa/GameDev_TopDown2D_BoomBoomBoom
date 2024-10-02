using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBombDamageable
{
    [Tooltip("leave it unassigned if u dont want this block to spawn any items after being destroyed")]
    [SerializeField] private List<GameObject> _hiddenItems;

    private void OnDrawGizmos()
    {
        if (_hiddenItems.Count != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
        }
    }

    public void TakeExplosionDamage(Vector3 pos)
    {
        if (_hiddenItems.Count != 0)
        {
            int i = Random.Range(0, _hiddenItems.Count);
            Instantiate(_hiddenItems[i], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}