using UnityEngine;

[CreateAssetMenu(fileName = "HeroStatus", menuName = "ScriptableObject/Hero")]
public class HeroBaseStatus : ScriptableObject
{
    public int HealthPoint;
    public int AttackPower;
    public float AttackRange;
}
