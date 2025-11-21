using System.Collections;
using UnityEngine;

public class StateIdle : ICharacterState
{
    BaseCharacter character;
    Coroutine routine;
    WaitForSeconds delay;
    public StateIdle(BaseCharacter controller)
    {
        character = controller;
        delay = new WaitForSeconds(0.2f);
    }

    public void OnEnter()
    {
        routine = character.StartCrt(OnUpdate());
    }

    // 빠져나갈 때 공격 타겟 탐색
    public void OnExit()
    {
        character.targetCombat = Physics2D.OverlapCircle(character.transform.position, 50f, character.layerMask);
        character.EndCrt(routine);
    }

    // 공격 받으면(체력이 깎이면) 상태 전이
    public IEnumerator OnUpdate()
    {
        while (character.status.HealthPoint == character.CurHealthPoint) 
        {
            yield return delay;
        }
        character.SetState(new StateCombat(character));
    }
}
