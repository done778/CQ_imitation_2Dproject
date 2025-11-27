using System.Collections;
using UnityEngine;

public class EnergyBall : MonoBehaviour, IEffectSkill
{
    int power;
    float moveSpeed = 10;
    Coroutine remainSkill;
    public void Init(int amount)
    {
        power = amount;
        remainSkill = StartCoroutine(RemainSkillObj());
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
            gameObject.SetActive(false);
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
