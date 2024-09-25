using UnityEngine;
using TMPro;
using System;


public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private CustomIntSO _clock;

    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _clockTickChannel;

    private void OnEnable()
    {
        _timerText.text = FormatSecondsToMinutes(_clock.Value);
        _clockTickChannel.OnEventRaised += UpdateTextValue;
    }
    
    private void UpdateTextValue(int value)
    {
        _timerText.text = FormatSecondsToMinutes(value);
    }

    private string FormatSecondsToMinutes(int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    private void OnDisable()
    {
        _clockTickChannel.OnEventRaised -= UpdateTextValue;
    }
}