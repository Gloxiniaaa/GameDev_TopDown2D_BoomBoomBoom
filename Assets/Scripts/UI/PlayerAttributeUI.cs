using TMPro;
using UnityEngine;

public class PlayerAttributeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _bombRangeText;
    [SerializeField] private TextMeshProUGUI _bombAmountText;
    [SerializeField] private TextMeshProUGUI _currentBombAmountText;
    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _updateSpeedUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombRangeUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombAmountUIChannel;
    [SerializeField] private IntEventChannelSO _currentBombLeftUIChannel;

    private int _bombAmount = 3;
    private int _amountLeft = 3;

    private void OnEnable()
    {
        _updateSpeedUIChannel.OnEventRaised += UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised += UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised += UpdateBombAmountUI;
        _currentBombLeftUIChannel.OnEventRaised += UpdateCurrentBombAmountUI;
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
        _bombAmount = value;
        _bombAmountText.text = value.ToString();
        _amountLeft++;
        _currentBombAmountText.text = _amountLeft.ToString();
    }

    private void UpdateCurrentBombAmountUI(int usedAmount)
    {
        _amountLeft = _bombAmount - usedAmount;
        _currentBombAmountText.text = _amountLeft.ToString();
    }

    private void OnDisable()
    {
        _updateSpeedUIChannel.OnEventRaised -= UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised -= UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised -= UpdateBombAmountUI;
        _currentBombLeftUIChannel.OnEventRaised -= UpdateCurrentBombAmountUI;
    }
}