using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonePattern <UIManager>
{
    private GameObject charSelectPanel;
    private GameObject pausePanel;
    
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
        pausePanel?.SetActive(false);
    }

    public void RegistCharSelectPanel(GameObject controller) {
        charSelectPanel = controller;
    }
    public void RegistPausePanel(GameObject controller) {
        pausePanel = controller;
    }
    public void SetCharSelectPanel(bool isActive) {
        charSelectPanel.SetActive(isActive);
    }
    public void SetPausePanel(bool isActive) {
        pausePanel.SetActive(isActive);
        if (isActive)
            GameManager.Instance.PauseGame();
        else
            GameManager.Instance.ResumeGame();
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
