using TMPro;
using UnityEngine;

public class PlayerAttributeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _bombRangeText;
    [SerializeField] private TextMeshProUGUI _bombAmountText;
    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _updateSpeedUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombRangeUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombAmountUIChannel;

    private void OnEnable()
    {
        _updateSpeedUIChannel.OnEventRaised += UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised += UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised += UpdateBombAmountUI;
    }

    private void UpdateSpeedUI(int value)
    {
        _speedText.text = value.ToString();
    }

    private void UpdateBombRangeUI(int value)
    {
        _bombRangeText.text = value.ToString();
    }

    private void UpdateBombAmountUI(int value)
    {
        _bombAmountText.text = value.ToString();
    }

    private void OnDisable()
    {
        _updateSpeedUIChannel.OnEventRaised -= UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised -= UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised -= UpdateBombAmountUI;
    }
}