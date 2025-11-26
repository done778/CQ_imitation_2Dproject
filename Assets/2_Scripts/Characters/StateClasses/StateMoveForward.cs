using System.Collections;
using UnityEngine;

public class StateMoveForward : ICharacterState
{
    BaseCharacter character;
    Coroutine routine;
    [SerializeField] float moveSpeed = 1.8f;

    public StateMoveForward(BaseCharacter controller)
    {
        character = controller;
    }

    public void OnEnter()
    {
        character.anim?.SetBool("isWalk", true);
        routine = character.StartCrt(OnUpdate());
    }

    public void OnExit()
    {
        character.anim?.SetBool("isWalk", false);
        character.EndCrt(routine);
    }

    public IEnumerator OnUpdate()
    {
        bool detectedOpponent = false;
        while (!detectedOpponent) {
            if (character is IUsableSkill)
            {
                if ( (character as IUsableSkill).IsWaitingSkill() )
                {
                    character.SetState(character.stateSkillCasting);
                }
            }
            character.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            detectedOpponent = character.DetectOpponent();
            yield return null;
        }
        character.SetState(character.stateCombat);
    }
}
