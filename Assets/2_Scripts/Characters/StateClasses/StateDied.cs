using System.Collections;
using UnityEngine;

public class StateDied : ICharacterState
{
    BaseCharacter hero;
    public StateDied(BaseCharacter controller)
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
