using UnityEngine;

[CreateAssetMenu(fileName = "skillData", menuName = "ScriptableObject/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [SerializeField] private float skillBasePower;
    [SerializeField] private GameObject skillPrefab;

    public GameObject SkillPrefab => skillPrefab;
    public float SkillBasePower => skillBasePower;
}
