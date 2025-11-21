using UnityEngine;

public class StateMoveForward : IHeroState
{
    HeroController hero;
    [SerializeField] float moveSpeed = 3f;

    public StateMoveForward(HeroController controller)
    {
        hero = controller;
    }

    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {
        hero.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    public void OnExit()
    {
        
    }
}
