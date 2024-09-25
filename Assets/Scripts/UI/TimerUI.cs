using UnityEngine;
using TMPro;


public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private CustomIntSO _clock;

    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _clockTickChannel;

    private void OnEnable()
    {
        _timerText.text = _clock.Value.ToString();
        _clockTickChannel.OnEventRaised += UpdateTextValue;
    }
    private void UpdateTextValue(int value)
    {
        _timerText.text = value.ToString();
    }

    private void OnDisable()
    {
        _clockTickChannel.OnEventRaised -= UpdateTextValue;
    }
}