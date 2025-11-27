using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicUiController : MonoBehaviour
{
    [SerializeField] private Image playerHpBar;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private Image progressBar;

    private void Awake()
    {
        BattleManager.Instance.changePlayerHp += PlayerHpUpdate;
        BattleManager.Instance.changeProgress += ProgressUpdate;
    }

    private void OnDestroy()
    {
        BattleManager.Instance.changePlayerHp -= PlayerHpUpdate;
        BattleManager.Instance.changeProgress -= ProgressUpdate;
    }

    public void PlayerHpUpdate(int cur, int max)
    {
        if (cur < 0) cur = 0;
        hpText.text = cur.ToString();
        playerHpBar.fillAmount = (float)cur / max;
    }
    public void ProgressUpdate(int cur, int max)
    {
        progressBar.fillAmount = (float)cur / max;
    }
}
