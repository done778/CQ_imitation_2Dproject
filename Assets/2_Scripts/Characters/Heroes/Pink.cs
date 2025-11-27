using UnityEngine;

public class Pink : BaseHero
{
    [SerializeField] private SkillDataSO SkillEffect;
    public override void SkillLogic(int chain)
    {
        BattleManager.Instance.Healing( (int)(SkillEffect.SkillBasePower * chainBonus[chain]) );
    }
}
