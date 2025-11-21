using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    protected int HealthPoint {  get; set; }
    protected int AttackPower {  get; set; }

    protected virtual void BaseAttack()
    {
        Debug.Log("평타");
    }
}
