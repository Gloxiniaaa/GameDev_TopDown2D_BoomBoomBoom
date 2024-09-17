using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "PlayerAttributeSO", order = 0)]
public class PlayerAttributeSO : ScriptableObject
{
    [SerializeField] private float _initSpeed;
    [SerializeField] private int _initBombAmount;
    public float Speed { get; private set; }
    public int BombAmount { get; private set; }
    public GameObject BombPrefab;

    [Header("Listen on channel:")]
    [SerializeField] private FloatEventChannelSO _speedupEvent;
    [SerializeField] private IntEventChannelSO _bombAmountUpEvent;

    private void OnEnable()
    {
        Speed = _initSpeed;
        BombAmount = _initBombAmount;
        _speedupEvent.OnEventRaised += IncreaseSpeed;
        _bombAmountUpEvent.OnEventRaised += IncreaseBombAmount;
    }

    private void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }

    private void IncreaseBombAmount(int amount)
    {
        BombAmount += amount;
    }

    private void OnDisable()
    {
        _speedupEvent.OnEventRaised -= IncreaseSpeed;
        _bombAmountUpEvent.OnEventRaised -= IncreaseBombAmount;
    }
}