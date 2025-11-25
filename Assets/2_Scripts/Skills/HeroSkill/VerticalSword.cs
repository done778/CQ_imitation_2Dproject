using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSword : MonoBehaviour
{
    int power;
    bool firstTarget;
    float groundedPos;
    public void Init(int amount)
    {
        power = amount;
        Destroy(gameObject, 1.5f);
        firstTarget = true;
        groundedPos = 0.6f;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < groundedPos)
        {
            Vector3 temp = transform.position;
            temp.y = groundedPos;
            transform.position = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstTarget)
        {
            if (collision.CompareTag("Enemy"))
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
