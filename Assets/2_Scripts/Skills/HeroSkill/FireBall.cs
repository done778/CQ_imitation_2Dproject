using UnityEngine;

public class FireBall : MonoBehaviour, IEffectSkill
{
    int power;
    float moveSpeed = 10;
    public void Init(int amount)
    {
        power = amount;
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
        }
    }
}
