using UnityEngine;

public class Green : BaseHero
{
    [SerializeField] private SkillDataSO SkillEffect;
    GameObject curSkill;

    public override void SkillLogic(int chain)
    {
        curSkill = Instantiate(SkillEffect.SkillPrefab);
        curSkill.GetComponent<IEffectSkill>().Init((int)(status.AttackPower * SkillEffect.SkillBasePower * chainBonus[chain]));
        Vector3 temp = transform.position;
        temp.x += 1.5f;
        temp.y += 7f;
        curSkill.transform.position = temp;
    }
}
