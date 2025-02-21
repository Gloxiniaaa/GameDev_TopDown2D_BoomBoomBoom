using UnityEngine;

[CreateAssetMenu(fileName = "CustomIntSO", menuName = "CustomIntSO", order = 0)]
public class CustomIntSO : DescriptionBaseSO
{
    [SerializeField] private int _initialValue;
    [SerializeField] private bool _resetOnEnable;
    public int Value { get; private set; }


    [Header("Broadcast on channel:")]
    [SerializeField] private IntEventChannelSO OnValueChanged;

    [Header("Listen on channel:")]
    [SerializeField] private VoidEventChannelSO _startLevelChannel;


    private void Awake()
    {
        Value = _initialValue;
    }

    private void OnEnable()
    {
        OnValueChanged.RaiseEvent(Value);
        _startLevelChannel.OnEventRaised += Reset;
    }

    private void Reset()
    {
        if (_resetOnEnable)
        {
            Value = _initialValue;
            OnValueChanged.RaiseEvent(Value);
        }
    }

    // public void Set(int val)
    // {
    //     if (Value != val)
    //     {
    //         Value = val;
    //         OnValueChanged.RaiseEvent(Value);
    //     }
    // }

    public void Add(int amount)
    {
        if (amount != 0)
        {
            Value += amount;
            OnValueChanged.RaiseEvent(Value);
        }
    }

    private void OnDisable()
    {
        _startLevelChannel.OnEventRaised += Reset;
    }
}