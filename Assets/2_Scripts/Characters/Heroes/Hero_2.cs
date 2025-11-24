using UnityEngine;

public class Hero_2 : BaseHero
{
    [SerializeField] private GameObject SkillEffect;
    GameObject curSkill;

    float skillPower = 0.6f;

    public Hero_2()
    {
        heroName = "영웅2";
    }
    public override void SkillLogic(int chain)
    {
        Debug.Log($"{heroName}, {chain} 체인으로 범위 공격 스킬 발동!");
        curSkill = Instantiate(SkillEffect);
        curSkill.GetComponent<EnergyBolt>().Init((int)(status.AttackPower * skillPower * chainBonus[chain]));
        Vector3 temp = transform.position;
        temp.x += 0.3f;
        curSkill.transform.position = temp;
    }
}
