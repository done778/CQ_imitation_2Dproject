using System.Collections;
using System.Collections.Generic;
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
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
