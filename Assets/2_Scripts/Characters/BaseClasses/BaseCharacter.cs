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
    private BattleManager battleManager;

    public int CurHealthPoint { get; set; }

    public void Init()
    {
        CurHealthPoint = status.HealthPoint;
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
        target.GetComponent<BaseCharacter>().TakeDamage(status.AttackPower);
        Debug.Log($"{gameObject.name}이(가) {target.name}을 공격");
    }

    // 피격 로직
    private void TakeDamage(int damage)
    {
        CurHealthPoint -= damage;
        if (CurHealthPoint <= 0)
        {
            Died();
        }
    }

    // 사망 로직
    private void Died()
    {
        Destroy(gameObject);
    }
}
