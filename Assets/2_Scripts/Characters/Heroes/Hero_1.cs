using System.Collections;
using UnityEngine;

public class Hero_1 : BaseHero
{
    [SerializeField] private GameObject SkillEffect;
    GameObject curSkill;

    float skillPower = 1.5f;

    public override void SkillLogic(int chain)
    {
        curSkill = Instantiate(SkillEffect);
        curSkill.GetComponent<VerticalSword>().Init((int)(status.AttackPower * skillPower * chainBonus[chain]));
        Vector3 temp = transform.position;
        temp.x += 3f;
        temp.y += 9f;
        curSkill.transform.position = temp;
    }
}
