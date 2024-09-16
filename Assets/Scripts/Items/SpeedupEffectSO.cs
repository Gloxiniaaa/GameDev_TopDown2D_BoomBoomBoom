using UnityEngine;

[CreateAssetMenu(fileName = "SpeedupEffectSO", menuName = "ItemEffect/SpeedupEffectSO", order = 0)]
public class SpeedupEffectSO : ItemEffectSO
{
    [Range(0.2f, 1f)]
    [SerializeField] private float _amountPerIncrease;

    [Header("Broadcast on channel:")]
    [SerializeField] private FloatEventChannelSO _speedupEvent;
    public override void ApllyEffect()
    {
        _speedupEvent.RaiseEvent(_amountPerIncrease);
    }
}