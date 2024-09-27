using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    [Header("Listen on channel:")]
    [SerializeField] private VoidEventChannelSO _LevelCompleteChannel;
    [SerializeField] private VoidEventChannelSO _timesupChannel;
    [SerializeField] protected VoidEventChannelSO _playerDeathChannel;

    private void Awake()
    {
        LoadUIScene();
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        _LevelCompleteChannel.OnEventRaised += LoadWinGameUI;
        _timesupChannel.OnEventRaised += LoadGameOverUI;
        _playerDeathChannel.OnEventRaised += LoadGameOverUI;
    }

    private void LoadUIScene()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void LoadWinGameUI()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    private void LoadGameOverUI()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _LevelCompleteChannel.OnEventRaised -= LoadWinGameUI;
        _timesupChannel.OnEventRaised -= LoadGameOverUI;
        _playerDeathChannel.OnEventRaised -= LoadGameOverUI;
    }
}