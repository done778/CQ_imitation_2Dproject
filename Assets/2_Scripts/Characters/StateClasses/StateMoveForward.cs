using System.Collections;
using UnityEngine;

public class StateMoveForward : ICharacterState
{
    BaseHero hero;
    Coroutine routine;
    [SerializeField] float moveSpeed = 3f;

    public StateMoveForward(BaseHero controller)
    {
        hero = controller;
    }

    public void OnEnter()
    {
        routine = hero.StartCrt(OnUpdate());
    }

    public void OnExit()
    {
        hero.EndCrt(routine);
    }

    public IEnumerator OnUpdate()
    {
        bool detectedEnemy = false;
        while (!detectedEnemy) {
            hero.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            detectedEnemy = hero.DetectEnemy();
            yield return null;
        }
        hero.SetState(new StateCombat(hero));
    }
}
