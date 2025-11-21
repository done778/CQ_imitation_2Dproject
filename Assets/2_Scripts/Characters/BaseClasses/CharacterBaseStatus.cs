using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "ScriptableObject/Character")]
public class CharacterBaseStatus : ScriptableObject
{
    public int HealthPoint;
    public int AttackPower;
    public float AttackRange;
    public float AttackSpeed;
}
