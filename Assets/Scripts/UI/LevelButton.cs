using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void EnterThisLevel()
    {
        SceneManager.LoadScene(_sceneName);
    }
}