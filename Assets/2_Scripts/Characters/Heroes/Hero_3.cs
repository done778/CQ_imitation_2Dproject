using UnityEngine;

public class Hero_3 : BaseHero
{
    public override void SkillLogic(int chain)
    {
        BattleManager.Instance.Healing( (int)(30 * chainBonus[chain]) );
    }
}
