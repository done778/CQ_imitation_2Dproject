
public class Pink : BaseHero
{
    public override void SkillLogic(int chain)
    {
        BattleManager.Instance.Healing( (int)(SkillEffect.SkillBasePower * BattleManager.Instance.chainBonus[chain]) );
    }
}
