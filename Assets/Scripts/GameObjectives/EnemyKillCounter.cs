using UnityEngine;

public class EnemyKillCounter : MonoBehaviour
{
    [SerializeField] private CustomIntSO _killCount;
    [SerializeField] private CustomIntSO _killObjective;


    [Header("Listen on channel:")]
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;


    private void OnEnable()
    {
        _enemyDeathChannel.OnEventRaised += IncreaseCount;
    }

    [ContextMenu("increase count")]
    private void IncreaseCount()
    {
        _killCount.Add(1);
        if (_killCount.Value >= _killObjective.Value)
        {
            Debug.Log("u meet the objective");
            // win game
        }
    }

    private void OnDisable()
    {
        _enemyDeathChannel.OnEventRaised -= IncreaseCount;
    }

}