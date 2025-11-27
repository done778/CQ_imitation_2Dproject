using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour, IEffectSkill
{
    int power;
    int count;
    float moveSpeed = 10;
    public void Init(int amount)
    {
        power = amount;
        count = 0;
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BattleManager.Instance.SkillAttackInteraction(
                collision.gameObject.GetComponent<BaseCharacter>(),
                power
                );
            count++;
            if (count > 1)
                Destroy(gameObject);
        }
    }
}
