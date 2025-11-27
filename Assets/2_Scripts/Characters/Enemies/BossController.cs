using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseBoss
{
    [SerializeField] private SkillDataSO SkillEffect;
    GameObject curSkill;

    WaitForSeconds delay;

    void Start()
    {
        delay = new WaitForSeconds(0.3f);
    }

    public override void SkillLogic(int chain)
    {
        StartCoroutine(BossSkill(chain+1));
    }

    private IEnumerator BossSkill(int chain)
    {
        for (int i = 0; i < chain; i++)
        {
            curSkill = Instantiate(SkillEffect.SkillPrefab);
            curSkill.GetComponent<FallingRock>().Init((int)(status.AttackPower * SkillEffect.SkillBasePower));
            Vector3 temp = new Vector3();
            if (targetCombat != null)
                temp = targetCombat.transform.position;
            else
            {
                temp = transform.position;
                temp.x -= 6f;
            }
            temp.y += 9f;
            temp.x += Random.Range(-2f, 2f);

            curSkill.transform.position = temp;
            yield return delay;
        }
        
    }
}
