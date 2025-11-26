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
        character.anim?.SetBool("isCombat", true);
        routine = character.StartCrt(OnUpdate());
    }

    public void OnExit()
    {
        character.anim?.SetBool("isCombat", false);
        character.EndCrt(routine);
    }

    // 일정 시간 간격으로 공격, 스킬을 사용하거나 공격 대상이 사라지면 탈출
    public IEnumerator OnUpdate()
    {
        // 아직 스킬 구현 전이라 공격 대상 사라지는 조건만 작성
        while (character.targetCombat != null) {
            if (character is IUsableSkill)
            {
                if ((character as IUsableSkill).IsWaitingSkill())
                {
                    character.SetState(character.stateSkillCasting);
                }
            }
            character.BaseAttack(character.targetCombat.gameObject);
            yield return delay;
        }
        character.SetState(character.stateMoveForward);
    }
}
