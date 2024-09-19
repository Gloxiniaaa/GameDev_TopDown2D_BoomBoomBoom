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

    private void OnEnable()
    {
        CountDownTime = _initCoutDownTime;
        Range = _initRange;
        _bombRangeUpEvent.OnEventRaised += IncreaseBombRange;
    }

    private void IncreaseBombRange(int amount)
    {
        Range += amount;
        _updateBombRangeUIChannel.RaiseEvent(Range);
    }

    private void OnDisable()
    {
        _bombRangeUpEvent.OnEventRaised -= IncreaseBombRange;
    }
}