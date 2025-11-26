using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    void Awake()
    {
        UIManager.Instance.RegistCharSelectPanel(gameObject);
    }

    public void OnBackButtonClick()
    {
        ClosePanel();
    }
    private void ClosePanel()
    {
        UIManager.Instance.SetCharSelectPanel(false);
    }

}
