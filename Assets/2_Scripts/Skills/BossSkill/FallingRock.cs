using System.Collections;
using UnityEngine;

public class FallingRock : MonoBehaviour, IEffectSkill
{
    int power;
    bool firstTarget;
    float groundedPos = -0.4f;
    Coroutine remainSkill;
    public void Init(int amount)
    {
        power = amount;
        firstTarget = true;
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
            if (collision.CompareTag("Player"))
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
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
    }
}
