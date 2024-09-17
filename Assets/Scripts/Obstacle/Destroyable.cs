using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [Tooltip("leave it unassigned if u dont want this block to spawn an item after being destroyed")]
    [SerializeField] private GameObject _hiddenItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.ExplosionTag))
        {
            if (_hiddenItem)
                Instantiate(_hiddenItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}