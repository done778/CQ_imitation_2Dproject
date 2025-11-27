using UnityEngine;

public class Orange : BaseHero
{
    public override void SkillLogic(int chain)
    {
        base.SkillLogic(chain);
        Vector3 temp = transform.position;
        temp.x += 0.3f;
        curSkill.transform.position = temp;
    }
}
