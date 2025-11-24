using System.Collections;
using UnityEngine;

public class Hero_1 : BaseHero
{
    [SerializeField] private GameObject SkillEffect;
    GameObject curSkill;

    float skillPower = 1.5f;

    public Hero_1()
    {
        heroName = "영웅1";
    }
    public override void SkillLogic(int chain)
    {
        Debug.Log($"{heroName}, {chain} 체인으로 단일 대상 스킬 발동!");
        curSkill = Instantiate(SkillEffect);
        curSkill.GetComponent<VerticalSword>().Init((int)(status.AttackPower * skillPower * chainBonus[chain]));
        Vector3 temp = transform.position;
        temp.x += 2f;
        temp.y += 9f;
        curSkill.transform.position = temp;
    }
}
