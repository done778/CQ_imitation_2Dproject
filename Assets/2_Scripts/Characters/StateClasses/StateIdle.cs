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

    public void OnExit()
    {
        character.EndCrt(routine);
    }

    public IEnumerator OnUpdate()
    {
        // 공격 받으면(체력이 깎이면) 상태 전이
        while (character.status.HealthPoint == character.CurHealthPoint) 
        {
            yield return delay;
        }
        // 자신의 공격 사거리 내 적이 있는지 판별
        bool isDetected = character.targetCombat = Physics2D.OverlapCircle(
            character.transform.position, 
            character.status.AttackRange, 
            character.layerMask
            );

        if ( isDetected ) // 감지 되었다면 전투 상태로 전환
            character.SetState(new StateCombat(character));
        else // 그렇지 않다면 앞으로 이동하여 탐색
            character.SetState(new StateMoveForward(character));
    }
}
