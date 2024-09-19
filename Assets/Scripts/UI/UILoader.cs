using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Awake()
    {
        LoadUIScene();
    }

    private void LoadUIScene()
    {
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }
}