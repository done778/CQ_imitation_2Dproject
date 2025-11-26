using System.Collections;
using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;

    void Awake()
    {
        UIManager.Instance.RegistCharSelectPanel(gameObject);
    }

    private void OnEnable()
    {
        warningPanel.SetActive(false);
    }

    public void OnBackButtonClick()
    {
        ClosePanel();
    }
    private void ClosePanel()
    {
        UIManager.Instance.SetCharSelectPanel(false);
    }
    public void PopUpWarningMessage()
    {
        StartCoroutine(PopUpWaringPanel());
    }
    private IEnumerator PopUpWaringPanel()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningPanel.SetActive(false);
    }
}
