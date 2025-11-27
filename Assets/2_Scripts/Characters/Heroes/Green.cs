using UnityEngine;

public class Green : BaseHero
{
    public override void SkillLogic(int chain)
    {
        base.SkillLogic(chain);
        Vector3 temp = transform.position;
        temp.x += 1.5f;
        temp.y += 7f;
        curSkill.transform.position = temp;
    }
}
