using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 베이스 영웅(플레이어 진영) 클래스. 초기 상태와 적 감지를 공통으로 가짐.
// 영웅 스킬은 이 클래스를 상속받는 각각의 영웅 클래스에서 구현

public abstract class BaseHero : BaseCharacter, IUsableSkill
{
    protected Queue<int> SkillQueue;
    protected GameObject curSkill;

    private bool isCasting = false;
    
    [SerializeField] protected SkillDataSO SkillEffect;
    public SkillDataSO SkillData => SkillEffect;
    void Awake()
    {
        Init();

        SkillQueue = new Queue<int>();

        stateMoveForward = new StateMoveForward(this);
        stateCombat = new StateCombat(this);
        stateSkillCasting = new StateSkillCasting(this);
        SetState(stateMoveForward);
    }

    public override void Died()
    {
        StartCoroutine(DiedAnim());
    }
    protected override IEnumerator DiedAnim()
    {
        anim.SetTrigger("isDied");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
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

    public virtual void SkillLogic(int chain)
    {
        curSkill = BattleManager.Instance.RequestSkillObj(status.UniqueId);
        curSkill.GetComponent<IEffectSkill>().Init((int)(status.AttackPower * SkillEffect.SkillBasePower * BattleManager.Instance.chainBonus[chain]));
    }
}
