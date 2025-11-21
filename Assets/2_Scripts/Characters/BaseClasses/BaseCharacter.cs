using System.Collections;
using UnityEngine;

// 베이스 캐릭터 클래스. 아군, 적 모두가 상속 받음.
// 공통 스탯과 상태 전이 메서드, 일반 공격 메서드 등이 있음.
public abstract class BaseCharacter : MonoBehaviour
{
    public CharacterBaseStatus status;
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

    public void BaseAttack(GameObject target)
    {
        target.GetComponent<BaseCharacter>().TakeDamage(status.AttackPower);
        Debug.Log($"{gameObject.name}이(가) {target.name}을 공격");
    }

    private void TakeDamage(int damage)
    {
        CurHealthPoint -= damage;
        if (CurHealthPoint <= 0)
        {
            Died();
        }
    }
    private void Died()
    {
        Time.timeScale = 0f;
        //Destroy(gameObject);
    }
}
