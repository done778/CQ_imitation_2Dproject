using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Image playerHpBar;
    [SerializeField] private TextMeshProUGUI hpText;

    private void Start()
    {
        BattleManager.Instance.changePlayerHp += PlayerHpUpdate;
    }

    public void PlayerHpUpdate(int cur, int max)
    {
        if (cur < 0) cur = 0;
        hpText.text = cur.ToString();
        playerHpBar.fillAmount = (float)cur / max;
    }
}
