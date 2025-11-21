using UnityEngine;

// 베이스 영웅(플레이어 진영) 클래스. 초기 상태와 적 감지를 공통으로 가짐.
// 영웅 스킬은 이 클래스를 상속받는 각각의 영웅 클래스에서 구현
public abstract class BaseHero : BaseCharacter
{
    void Start()
    {
        Init();
        SetState(new StateMoveForward(this));
    }

    public bool DetectEnemy()
    {
        targetCombat = Physics2D.OverlapCircle(transform.position, status.AttackRange, layerMask);
        if (targetCombat != null)
        {
            return true;
        }
        return false;
    }
    protected abstract void HeroSkill();
}
