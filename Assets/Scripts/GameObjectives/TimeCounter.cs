using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private CustomIntSO _clock;

    [SerializeField] private AudioGroupSO _timesupSfx;
    [SerializeField] private AudioGroupSO _clockTickSfx;

    [Header("Broadcast on channel:")]
    [SerializeField] private VoidEventChannelSO _timesupChannel;
    [SerializeField] private AudioEventChannelSO _sfxChannel;


    private void OnEnable()
    {
        CountDown();
    }

    private void CountDown()
    {
        InvokeRepeating(nameof(Tick), 1f, 1f);
    }

    private void Tick()
    {
        _clock.Add(-1);
        if (_clock.Value == 0)
        {
            _sfxChannel.RaiseEvent(_timesupSfx);
            _timesupChannel.RaiseEvent();
            CancelInvoke();
        }
        else if (_clock.Value == 4)
        {
            _sfxChannel.RaiseEvent(_clockTickSfx);
        }
    }
}