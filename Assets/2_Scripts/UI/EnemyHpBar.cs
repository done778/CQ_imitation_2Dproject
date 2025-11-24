using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Image enemyHpBar;

    public void EnemyHpUpdate(int cur, int max)
    {
        enemyHpBar.fillAmount = (float)cur / max;
    }
}
