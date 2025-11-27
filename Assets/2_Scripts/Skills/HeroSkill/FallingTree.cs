using System.Collections;
using UnityEngine;

public class FallingTree : MonoBehaviour, IEffectSkill
{
    int power;
    bool firstTarget;
    float groundedPos;
    Coroutine remainSkill;
    public void Init(int amount)
    {
        power = amount;
        firstTarget = true;
        groundedPos = 0.6f;
        remainSkill = StartCoroutine(RemainSkillObj());
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

    private void OnDisable()
    {
        if (remainSkill != null)
        {
            StopCoroutine(remainSkill);
            remainSkill = null;
        }
    }
    private IEnumerator RemainSkillObj()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
