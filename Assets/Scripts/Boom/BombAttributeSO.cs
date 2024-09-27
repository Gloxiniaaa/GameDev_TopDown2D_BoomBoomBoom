using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BombAttributeSO", menuName = "BombAttributeSO", order = 0)]
public class BombAttributeSO : ScriptableObject
{
    [SerializeField] private float _initCoutDownTime;
    [SerializeField] private int _initRange;
    public float CountDownTime { get; private set; }
    public int Range { get; private set; }
    public GameObject ExplosionPrefab;

    [Header("Broadcast on channel:")]
    [SerializeField] private IntEventChannelSO _updateBombRangeUIChannel;

    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _bombRangeUpEvent;
    [SerializeField] private VoidEventChannelSO _startLevelChannel;


    private void OnEnable()
    {
        _bombRangeUpEvent.OnEventRaised += IncreaseBombRange;
        _startLevelChannel.OnEventRaised += ResetValue;
    }

    public void ResetValue()
    {
        CountDownTime = _initCoutDownTime;
        Range = _initRange;
        _updateBombRangeUIChannel.RaiseEvent(Range);
    }

    private void IncreaseBombRange(int amount)
    {
        Range += amount;
        _updateBombRangeUIChannel.RaiseEvent(Range);
    }

    private void OnDisable()
    {
        _bombRangeUpEvent.OnEventRaised -= IncreaseBombRange;
        _startLevelChannel.OnEventRaised -= ResetValue;
    }
}