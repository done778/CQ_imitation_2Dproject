using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 베이스 보스(적 진영) 클래스.
// 보스의 특수 공격은 각각의 보스 클래스에 구현.

public abstract class BaseBoss : EnemyController, IUsableSkill
{
    protected string bossName;
    protected Queue<int> SkillQueue;
    private bool isCasting = false;
    private bool firstCasting;
    private bool secondCasting;
    private bool thirdCasting;

    [SerializeField] protected SkillDataSO SkillEffect;
    public SkillDataSO SkillData => SkillEffect;

    protected override void Awake()
    {
        SkillQueue = new Queue<int>();
        stateMoveForward = new StateMoveForward(this);
        stateCombat = new StateCombat(this);
        stateSkillCasting = new StateSkillCasting(this);
        firstCasting = true;
        secondCasting = true;
        thirdCasting = true;
    }

    protected override void OnEnable()
    {
        Init();
        SetState(stateMoveForward);
    }

    // 보스의 피격 로직에 남은 HP량에 따른 특수 공격 로직 추가
    // 체력이 적을 때 발동하는 공격이 좀 더 강한 위력을 가짐
    public override void TakeDamage(int damage)
    {
        CurHealthPoint -= damage;

        float curHpRate = CurHealthPoint / (float)status.HealthPoint;
        if (curHpRate <= 0.8 && firstCasting)
        {
            RegistSkillQueue(1);
            firstCasting = false;
        }

        else if (curHpRate <= 0.5 && secondCasting)
        {
            RegistSkillQueue(2);
            secondCasting = false;
        }

        else if (curHpRate <= 0.2 && thirdCasting)
        {
            RegistSkillQueue(3);
            thirdCasting = false;
        }

        else if (CurHealthPoint <= 0)
        {
            Died();
        }
        EnemyHpUpdate(CurHealthPoint, status.HealthPoint);
    }

    public override void Died()
    {
        StartCoroutine(DiedAnim());
    }

    protected override IEnumerator DiedAnim()
    {
        anim.SetTrigger("isDied");
        yield return new WaitForSeconds(0.8f);
        // 죽을 때 파괴되지 말고 일단 배틀매니저한테 나 죽었다고 알리기.
        BattleManager.Instance.BossDied();
    }

    public void RegistSkillQueue(int chain)
    {
        SkillQueue.Enqueue(chain);
    }

    public bool IsWaitingSkill()
    {
        if (SkillQueue.Count > 0)
            return true;
        else
            return false;
    }

    public void CastSkillQueue()
    {
        isCasting = true;
        SkillLogic(SkillQueue.Dequeue());
        isCasting = false;
    }

    public IUsableSkill GetClass()
    {
        return this;
    }

    public bool IsCasting()
    {
        return isCasting;
    }

    public abstract void SkillLogic(int chain);
}
