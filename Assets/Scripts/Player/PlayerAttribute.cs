using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "PlayerAttributeSO", order = 0)]
public class PlayerAttributeSO : ScriptableObject
{
    [SerializeField] private float _initSpeed;
    [SerializeField] private int _initBombAmount;
    [HideInInspector] public float Speed;
    [HideInInspector] public int BombAmount;
    public GameObject BombPrefab;

    [Header("Listen on channel:")]
    [SerializeField] private FloatEventChannelSO _speedupEvent;

    private void OnEnable()
    {
        Speed = _initSpeed;
        BombAmount = _initBombAmount;
        _speedupEvent.OnEventRaised += IncreaseSpeed;
    }

    private void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }

    private void OnDisable()
    {
        _speedupEvent.OnEventRaised -= IncreaseSpeed;
    }
}