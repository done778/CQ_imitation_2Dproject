using System.Collections;
using UnityEngine;

public class StateSkillCasting : ICharacterState
{
    BaseHero hero;
    public StateSkillCasting(BaseHero controller)
    {
        hero = controller;
    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {
        
    }
    public IEnumerator OnUpdate()
    {
        yield return null;
    }
}
