using System.Collections.Generic;

// 베이스 보스(적 진영) 클래스.
// 보스의 특수 공격은 각각의 보스 클래스에 구현.

public abstract class BaseBoss : BaseCaster
{
    protected string bossName;

    void Start()
    {
        Init();
        SkillQueue = new Queue<int>();
        stateMoveForward = new StateMoveForward(this);
        stateCombat = new StateCombat(this);
        stateSkillCasting = new StateSkillCasting(this);
        SetState(stateMoveForward);
    }
}
