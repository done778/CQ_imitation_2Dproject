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
        if (CheckHeroEntry())
        {
            GameManager.Instance.LoadScene("Stage");
            GameManager.Instance.ResumeGame();
        }
        else
        {
            UIManager.Instance.PopUpSetHeroWarningMessage();
        }
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
    public bool CheckHeroEntry()
    {
        var getInfo = BattleManager.Instance.GetHeroEntry();
        foreach (var id in getInfo) {
            if (id == null)
            {
                return false;
            }
        }
        return true;
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
