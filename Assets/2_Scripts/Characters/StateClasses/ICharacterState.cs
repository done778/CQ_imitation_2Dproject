using System.Collections;

public interface ICharacterState
{
    public void OnEnter();

    public IEnumerator OnUpdate();

    public void OnExit();
}
