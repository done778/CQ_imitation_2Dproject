using System.Collections;
using UnityEngine;

// 전투 상태 (평타 공격)
public class StateCombat : ICharacterState
{
    BaseCharacter character;
    Coroutine routine;
    WaitForSeconds delay;
    public StateCombat(BaseCharacter controller)
    {
        character = controller;
        delay = new WaitForSeconds(character.status.AttackSpeed);
    }
    public void OnEnter()
    {
        routine = character.StartCrt(OnUpdate());
    }

    public void OnExit()
    {
        character.EndCrt(routine);
    }
    public IEnumerator OnUpdate()
    {
        while (true) {
            character.BaseAttack(character.targetCombat.gameObject);
            yield return delay;
        }
    }
}
