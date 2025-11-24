using UnityEngine;

public class Hero_2 : BaseHero
{
    public Hero_2()
    {
        heroName = "영웅2";
    }
    public override void SkillLogic(int chain)
    {
        Debug.Log($"{heroName}, {chain} 체인으로 스킬 발동!");
    }
}
