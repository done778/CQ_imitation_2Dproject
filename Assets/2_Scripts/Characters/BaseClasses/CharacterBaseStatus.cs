using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "ScriptableObject/Character")]
public class CharacterBaseStatus : ScriptableObject
{
    [SerializeField] private int uniqueId;
    public GameObject prefab;
    public int HealthPoint;
    public int AttackPower;
    public float AttackRange;
    public float AttackSpeed;

    public int UniqueId => uniqueId;
}
