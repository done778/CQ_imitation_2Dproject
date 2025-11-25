using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonePattern<GameManager>
{
    bool isLoading;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void LoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(sceneName);
        isLoading = false;
    }
}
