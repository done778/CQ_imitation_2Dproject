using UnityEngine;

public class Hero_1 : BaseHero
{
    public Hero_1()
    {
        heroName = "영웅1";
    }
    public override void SkillLogic(int chain)
    {
        Debug.Log($"{heroName}, {chain} 체인으로 스킬 발동!");
    }
}
