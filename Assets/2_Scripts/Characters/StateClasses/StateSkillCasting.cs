using System.Collections;
using UnityEngine;

public class StateSkillCasting : ICharacterState
{
    BaseCharacter character;
    IUsableSkill caster;

    Coroutine routine;
    WaitForSeconds delay;

    public StateSkillCasting(BaseCharacter controller)
    {
        character = controller;
        delay = new WaitForSeconds(character.status.AttackSpeed);
    }
    public void OnEnter()
    {
        if (character is IUsableSkill) {
            caster = (character as IUsableSkill).GetClass();
        }
        routine = character.StartCrt(OnUpdate());
    }
    public void OnExit()
    {
        character.EndCrt(routine);
    }
    public IEnumerator OnUpdate()
    {
        caster.CastSkillQueue();

        yield return delay;

        if (character.targetCombat != null)
        {
            character.SetState(character.stateCombat);
        }
        else
        {
            character.SetState(character.stateMoveForward);
        }
    }
}
