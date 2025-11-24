using System.Collections.Generic;

// 베이스 영웅(플레이어 진영) 클래스. 초기 상태와 적 감지를 공통으로 가짐.
// 영웅 스킬은 이 클래스를 상속받는 각각의 영웅 클래스에서 구현

public abstract class BaseHero : BaseCaster
{
    protected string heroName;

    public Dictionary<int, float> chainBonus;

    void Awake()
    {
        Init();
        SkillQueue = new Queue<int>();
        chainBonus = new Dictionary<int, float>();

        chainBonus[1] = 1f;
        chainBonus[2] = 2.2f;
        chainBonus[3] = 4.5f;

        stateMoveForward = new StateMoveForward(this);
        stateCombat = new StateCombat(this);
        stateSkillCasting = new StateSkillCasting(this);
        SetState(stateMoveForward);
    }
}
