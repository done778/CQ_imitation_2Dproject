// 모든 일반 몹이 가지는 컴포넌트.
// 상태는 두 가지만 있음. Idle <-> Combat
// 플레이어에게 공격을 받으면 전투 상태로 전이함.
public class NormalEnemyController : BaseCharacter
{
    void Start()
    {
        Init();
        SetState(new StateIdle(this));
    }
}
