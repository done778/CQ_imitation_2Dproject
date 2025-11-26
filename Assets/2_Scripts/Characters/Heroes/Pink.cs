using UnityEngine;

public class Pink : BaseHero
{
    public override void SkillLogic(int chain)
    {
        BattleManager.Instance.Healing( (int)(30 * chainBonus[chain]) );
    }
}
