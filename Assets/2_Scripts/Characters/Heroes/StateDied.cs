using UnityEngine;

public class StateDied : IHeroState
{
    HeroController hero;
    public StateDied(HeroController controller)
    {
        hero = controller;
    }
    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {

    }
    public void OnExit()
    {
        
    }
}
