using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : MonoBehaviour
{
    int power;
    float moveSpeed = 10;
    public void Init(int amount)
    {
        power = amount;
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("에너지볼트 충돌 확인");
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("에너지볼트 적과 충돌 확인");
            BattleManager.Instance.SkillAttackInteraction(
                collision.gameObject.GetComponent<BaseCharacter>(),
                power
                );
        }
    }
}
