
// 스킬을 가진 클래스가 가질 인터페이스
public interface IUsableSkill
{
    // 스킬을 등록, 큐에 등록된 스킬 확인, 스킬 시전, 스킬 로직
    public void RegistSkillQueue(int chain);
    public bool IsWaitingSkill();
    public void CastSkillQueue();
    public void SkillLogic(int chain);

    public bool IsCasting();

    public IUsableSkill GetClass();
}
