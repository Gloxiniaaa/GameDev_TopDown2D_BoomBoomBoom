using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeUI : MonoBehaviour
{
    [SerializeField] private PlayerAttributeSO _playerStats;
    [SerializeField] private BombAttributeSO _bombStats;

    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _bombRangeText;
    [SerializeField] private TextMeshProUGUI _bombAmountText;
    [SerializeField] private TextMeshProUGUI _currentBombAmountText;
    [SerializeField] private Slider _healthSlider;


    [Header("Listen on channel:")]
    [SerializeField] private IntEventChannelSO _updateSpeedUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombRangeUIChannel;
    [SerializeField] private IntEventChannelSO _updateBombAmountUIChannel;
    [SerializeField] private IntEventChannelSO _currentBombLeftUIChannel;
    [SerializeField] private IntEventChannelSO _updateHeathUIChannel;


    private int _bombAmount = 3;
    private int _amountLeft = 3;

    private void OnEnable()
    {
        _updateSpeedUIChannel.OnEventRaised += UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised += UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised += UpdateBombAmountUI;
        _currentBombLeftUIChannel.OnEventRaised += UpdateCurrentBombAmountUI;
        _updateHeathUIChannel.OnEventRaised += UpdateHealthUI;

        InitilizeValue();
    }


    private void InitilizeValue()
    {
        _playerStats.ResetValue();
        _bombStats.ResetValue();
        _speedText.text = _playerStats.Speed.ToString();
        _bombAmountText.text = _playerStats.BombAmount.ToString();
        _currentBombAmountText.text = _playerStats.BombAmount.ToString();
        _bombRangeText.text = _bombStats.Range.ToString();
        
        _healthSlider.value = _playerStats.Health;
        _healthSlider.maxValue = _playerStats.Health;
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

    private void UpdateHealthUI(int value)
    {
        _healthSlider.value = value;
    }



    private void OnDisable()
    {
        _updateSpeedUIChannel.OnEventRaised -= UpdateSpeedUI;
        _updateBombRangeUIChannel.OnEventRaised -= UpdateBombRangeUI;
        _updateBombAmountUIChannel.OnEventRaised -= UpdateBombAmountUI;
        _currentBombLeftUIChannel.OnEventRaised -= UpdateCurrentBombAmountUI;
        _updateHeathUIChannel.OnEventRaised -= UpdateHealthUI;
    }
}