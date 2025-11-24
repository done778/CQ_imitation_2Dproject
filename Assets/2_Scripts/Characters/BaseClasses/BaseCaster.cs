using System.Collections.Generic;

// 스킬을 사용할 수 있는 객체가 이 클래스를 상속받음. 스킬 관련 로직을 포함한다.

public abstract class BaseCaster : BaseCharacter, IUsableSkill
{
    protected Queue<int> SkillQueue;
    private bool isCasting = false;

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
