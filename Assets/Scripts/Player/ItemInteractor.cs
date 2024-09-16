using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.ItemTag))
        {
            other.GetComponent<Item>().ApllyEffect();
        }
    }
}