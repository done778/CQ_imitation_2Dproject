using UnityEngine;

public class HeroController : MonoBehaviour
{
    private IHeroState curState;
    public HeroBaseStatus status;
    void Start()
    {
        SetState(new StateMoveForward(this));
        transform.GetComponentInChildren<CircleCollider2D>().radius = status.AttackRange;
    }
    void Update()
    {
        curState.OnUpdate();
    }

    public void SetState(IHeroState changeState)
    {
        curState?.OnExit();
        curState = changeState;
        curState.OnEnter();
    }
}
