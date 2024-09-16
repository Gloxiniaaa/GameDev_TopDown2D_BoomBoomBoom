using UnityEngine;

[CreateAssetMenu(fileName = "BombAmountUpSO", menuName = "ItemEffect/BombAmountUpSO", order = 2)]
public class BombAmountUpSO : ItemEffectSO
{
    [Header("Broadcast on channel:")]
    [SerializeField] private IntEventChannelSO _bombAmountUpEvent;

    public override void ApllyEffect()
    {
        _bombAmountUpEvent.RaiseEvent(1);
    }
}