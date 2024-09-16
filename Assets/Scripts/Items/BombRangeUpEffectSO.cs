using UnityEngine;

[CreateAssetMenu(fileName = "BombRangeUpEffectSO", menuName = "ItemEffect/BombRangeUpEffectSO", order = 1)]
public class BombRangeUpEffectSO : ItemEffectSO
{
    [Header("Broadcast on channel:")]
    [SerializeField] private IntEventChannelSO _bombRangeUpEvent;

    public override void ApllyEffect()
    {
        _bombRangeUpEvent.RaiseEvent(1);
    }
}