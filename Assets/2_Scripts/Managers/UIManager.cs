using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonePattern <UIManager>
{
    private GameObject charSelectPanel;
    private GameObject pausePanel;
    private GameObject stageClearPanel;
    private GameObject gameOverPanel;
    
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    public void EnterLobby()
    {
        charSelectPanel?.SetActive(false);
    }
    public void GameStart()
    {
        pausePanel = GameObject.Find("PausePanel");
        stageClearPanel = GameObject.Find("StageClearPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        pausePanel?.SetActive(false);
        stageClearPanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
    }

    public void RegistCharSelectPanel(GameObject controller) {
        charSelectPanel = controller;
    }

    public void SetCharSelectPanel(bool isActive) {
        charSelectPanel.SetActive(isActive);
    }
    public void SetPausePanel(bool isActive) 
    {
        pausePanel.SetActive(isActive);
        if (isActive)
            GameManager.Instance.PauseGame();
        else
            GameManager.Instance.ResumeGame();
    }

    public void SetStageClearPanel(bool isActive)
    {
        stageClearPanel.SetActive(isActive);
    }

    public void SetGameOverPanel(bool isActive)
    {
        gameOverPanel.SetActive(isActive);
    }

    public void PopUpSetHeroWarningMessage()
    {
        charSelectPanel.GetComponent<CharSelectPanel>().PopUpWarningMessage();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Stage"))
        {
            GameStart();
        }
        if (scene.name == "Lobby")
        {
            EnterLobby();
        }
    }
}
