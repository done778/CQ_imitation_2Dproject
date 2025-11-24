// 모든 일반 몹이 가지는 컴포넌트.
// 초기엔 Idle 상태로 정지 상태
// 공격 받을 시 주변 영웅 탐색
// 자신의 공격 사거리 내 영웅이 있다면 Combat으로, 없다면 전진하며 탐색.
// 플레이어에게 공격을 받으면 전투 상태로 전이함.
public class NormalEnemyController : BaseCharacter
{
    void Start()
    {
        Init();
        stateIdle = new StateIdle(this);
        stateMoveForward = new StateMoveForward(this);
        stateCombat = new StateCombat(this);
        stateSkillCasting = new StateSkillCasting(this);
        SetState(stateIdle);
    }
}
