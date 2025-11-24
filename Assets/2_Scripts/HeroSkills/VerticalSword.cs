using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSword : MonoBehaviour
{
    int power;
    bool firstTarget;
    public void Init(int amount)
    {
        power = amount;
        Destroy(gameObject, 1.5f);
        firstTarget = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0.6)
        {
            Vector3 temp = transform.position;
            temp.y = 0.6f;
            transform.position = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstTarget)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
            {
                BattleManager.Instance.SkillAttackInteraction(
                    collision.gameObject.GetComponent<BaseCharacter>(),
                    power
                    );
                firstTarget = false;
            }
        }
    }
}
