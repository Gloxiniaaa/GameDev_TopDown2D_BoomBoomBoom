using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKillCounter : MonoBehaviour
{
    [SerializeField] private CustomIntSO _killCount;
    [SerializeField] private CustomIntSO _killObjective;


    [Header("Listen on channel:")]
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;

    [Header("Broadcast on channel:")]
    [SerializeField] private VoidEventChannelSO _LevelCompleteChannel;


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
            _LevelCompleteChannel.RaiseEvent();
        }
    }

    private void OnDisable()
    {
        _enemyDeathChannel.OnEventRaised -= IncreaseCount;
    }

}