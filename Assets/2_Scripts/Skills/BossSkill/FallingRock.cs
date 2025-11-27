using UnityEngine;

public class FallingRock : MonoBehaviour, IEffectSkill
{
    int power;
    bool firstTarget;
    float groundedPos;
    public void Init(int amount)
    {
        power = amount;
        Destroy(gameObject, 1.5f);
        firstTarget = true;
        groundedPos = -0.4f;
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
}
