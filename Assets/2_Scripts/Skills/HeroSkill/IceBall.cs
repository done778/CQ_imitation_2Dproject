using System.Collections;
using UnityEngine;

public class IceBall : MonoBehaviour, IEffectSkill
{
    int power;
    int count;
    float moveSpeed = 10;
    Coroutine remainSkill;
    public void Init(int amount)
    {
        power = amount;
        count = 0;
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
            count++;
            if (count > 1)
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
