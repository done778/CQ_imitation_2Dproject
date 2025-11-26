using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void LoadTitleScene()
    {
        GameManager.Instance.LoadScene("Title");
    }
    public void LoadLobbyScene()
    {
        GameManager.Instance.LoadScene("Lobby");
    }
    public void LoadStageScene()
    {
        GameManager.Instance.LoadScene("Stage");
        GameManager.Instance.ResumeGame();
    }
    public void OpenCharSelectPanel()
    {
        UIManager.Instance.SetCharSelectPanel(true);
    }
    public void OpenPausePanel()
    {
        UIManager.Instance.SetPausePanel(true);
    }
    public void ContinueButton()
    {
        UIManager.Instance.SetPausePanel(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
