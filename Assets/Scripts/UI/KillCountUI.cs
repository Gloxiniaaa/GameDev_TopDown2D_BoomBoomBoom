using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private CustomIntSO _killObjective;
    [SerializeField] private CustomIntSO _killCount;
    [SerializeField] private TextMeshProUGUI _killObjectiveText;
    [SerializeField] private TextMeshProUGUI _killCountText;

    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _killObjectiveChanedChannel;
    [SerializeField] private IntEventChannelSO _killCountChangedChannel;

    private void OnEnable()
    {
        UpdateKillObjecitve(_killObjective.Value);
        UpdateKillCount(_killCount.Value);

        _killObjectiveChanedChannel.OnEventRaised += UpdateKillObjecitve;
        _killCountChangedChannel.OnEventRaised += UpdateKillCount;
    }

    private void UpdateKillObjecitve(int value)
    {
        _killObjectiveText.text = value.ToString();
    }

    private void UpdateKillCount(int value)
    {
        _killCountText.text = value.ToString();
    }

    private void OnDestroy()
    {
        _killObjectiveChanedChannel.OnEventRaised -= UpdateKillObjecitve;
        _killCountChangedChannel.OnEventRaised -= UpdateKillCount;
    }
}