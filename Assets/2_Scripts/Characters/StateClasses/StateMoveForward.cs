using System.Collections;
using UnityEngine;

public class StateMoveForward : ICharacterState
{
    BaseCharacter character;
    Coroutine routine;
    [SerializeField] float moveSpeed = 3f;

    public StateMoveForward(BaseCharacter controller)
    {
        character = controller;
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
        bool detectedOpponent = false;
        while (!detectedOpponent) {
            character.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            detectedOpponent = character.DetectOpponent();
            yield return null;
        }
        character.SetState(new StateCombat(character));
    }
}
