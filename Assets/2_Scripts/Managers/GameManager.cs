using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonePattern<GameManager>
{
    bool isLoading;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    // 씬 전환
    public void LoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(sceneName);
        isLoading = false;
    }

    // 게임 일시 정지
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // 게임 재개
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    // 스테이지 클리어 실패
    public void FailedGame()
    {
        UIManager.Instance.SetGameOverPanel(true);
        Time.timeScale = 0f;
    }

    // 스테이지 클리어 성공
    public void ClearGame()
    {
        UIManager.Instance.SetStageClearPanel(true);
        Time.timeScale = 0f;
    }
}
