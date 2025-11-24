using UnityEngine;

public class Hero_3 : BaseHero
{
    public Hero_3()
    {
        heroName = "영웅3";
    }
    public override void SkillLogic(int chain)
    {
        Debug.Log($"{heroName}, {chain} 체인으로 스킬 발동!");
    }
}
