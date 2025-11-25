using UnityEngine;

public class Hero_2 : BaseHero
{
    [SerializeField] private GameObject SkillEffect;
    GameObject curSkill;

    float skillPower = 0.6f;

    public override void SkillLogic(int chain)
    {
        curSkill = Instantiate(SkillEffect);
        curSkill.GetComponent<EnergyBolt>().Init((int)(status.AttackPower * skillPower * chainBonus[chain]));
        Vector3 temp = transform.position;
        temp.x += 0.3f;
        curSkill.transform.position = temp;
    }
}
