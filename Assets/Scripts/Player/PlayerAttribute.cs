using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "PlayerAttributeSO", order = 0)]
public class PlayerAttributeSO : ScriptableObject
{
    [SerializeField] private float _initSpeed;
    [SerializeField] private int _initBombAmount;
    [SerializeField] private int _initHealth;
    public float Speed { get; private set; }
    public int BombAmount { get; private set; }
    public int Health { get; private set; }
    public GameObject BombPrefab;

    [Header("Broadcast on channel:")]
    [SerializeField] private IntEventChannelSO _updateSpeedUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombAmountUIChannel;
    [SerializeField] private IntEventChannelSO _updateHeathUIChannel;

    [Header("Listen on channel:")]
    [SerializeField] private FloatEventChannelSO _speedupEvent;
    [SerializeField] private IntEventChannelSO _bombAmountUpEvent;
    [SerializeField] private IntEventChannelSO _playerGetsHurtChannel;

    private void OnEnable()
    {
        Speed = _initSpeed;
        BombAmount = _initBombAmount;
        Health = _initHealth;
        _speedupEvent.OnEventRaised += IncreaseSpeed;
        _bombAmountUpEvent.OnEventRaised += IncreaseBombAmount;
        _playerGetsHurtChannel.OnEventRaised += DecreaseHealth;
    }

    private void DecreaseHealth(int amount)
    {
        Health -= amount;
        _updateHeathUIChannel.RaiseEvent(Health);
    }

    private void IncreaseSpeed(float amount)
    {
        Speed += amount;
        int scaleSpeed = (int)Speed + (int)((Speed - _initSpeed) / amount);
        _updateSpeedUIChannel.RaiseEvent(scaleSpeed);
    }

    private void IncreaseBombAmount(int amount)
    {
        BombAmount += amount;
        _updateBombAmountUIChannel.RaiseEvent(BombAmount);
    }

    private void OnDisable()
    {
        _speedupEvent.OnEventRaised -= IncreaseSpeed;
        _bombAmountUpEvent.OnEventRaised -= IncreaseBombAmount;
        _playerGetsHurtChannel.OnEventRaised -= DecreaseHealth;
    }
}