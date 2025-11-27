using System;
using System.Collections;
using UnityEngine;

// 베이스 캐릭터 클래스. 아군, 적 모두가 상속 받음.
// 공통 스탯과 상태 전이 메서드, 일반 공격 메서드 등이 있음.
public abstract class BaseCharacter : MonoBehaviour
{
    public CharacterBaseStatus status;
    public StateIdle stateIdle;
    public StateMoveForward stateMoveForward;
    public StateCombat stateCombat;
    public StateSkillCasting stateSkillCasting;

    private ICharacterState curState;
    [SerializeField] public LayerMask layerMask;
    [HideInInspector] public Collider2D targetCombat;
    public Animator anim;

    public int CurHealthPoint { get; set; }

    public void Init()
    {
        CurHealthPoint = status.HealthPoint;
        anim = transform.GetComponentInChildren<Animator>();
    }

    public void SetState(ICharacterState changeState)
    {
        curState?.OnExit();
        curState = changeState;
        curState.OnEnter();
    }

    public Coroutine StartCrt(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void EndCrt(Coroutine routine)
    {
        if (routine != null)
            StopCoroutine(routine);
    }

    // 자신의 적을 탐지하는 로직 (적은 아군을, 아군은 적을)
    public bool DetectOpponent()
    {
        targetCombat = Physics2D.OverlapCircle(
            transform.position, 
            status.AttackRange, 
            layerMask
            );

        if (targetCombat != null)
        {
            return true;
        }
        return false;
    }

    // 일반 공격
    public void BaseAttack(GameObject target)
    {
        BaseCharacter hit = target.GetComponent<BaseCharacter>();
        BattleManager.Instance.BaseAttackInteraction(this, hit);
    }

    // 사망 로직
    public virtual void Died()
    {
        StartCoroutine(DiedAnim());
    }

    protected virtual IEnumerator DiedAnim()
    {
        anim.SetTrigger("isDied");
        yield return new WaitForSeconds(0.8f);
        // 죽을 때 파괴되지 말고 일단 배틀매니저한테 나 죽었다고 알리기.
        BattleManager.Instance.NormalEnemyDied(gameObject);
    }
}
