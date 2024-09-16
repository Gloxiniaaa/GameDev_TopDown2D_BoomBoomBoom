using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemEffectSO _itemEffect;

    public void ApllyEffect()
    {
        _itemEffect.ApllyEffect();
        Destroy(gameObject);
    }
}