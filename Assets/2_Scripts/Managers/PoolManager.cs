using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    private int numOfNormalEnemy = 4;
    private int numOfSkillObject = 5;
    private GameObject[] normalEnemyPool;
    private GameObject[,] heroSkillPool;
    private int[] indexOfSkillObject;

    private int numOfBossSkillObj = 6;
    private GameObject bossObject;
    private GameObject[] bossSkillPool;

    private Transform pos;

    private void Awake()
    {
        BattleManager.Instance.RegistPoolManager(this);
        normalEnemyPool = new GameObject[numOfNormalEnemy];
        bossSkillPool = new GameObject[numOfBossSkillObj];
        
        // 일반 적 풀 생성
        for (int i = 0; i < numOfNormalEnemy; i++)
        {
            normalEnemyPool[i] = Instantiate(normalEnemyPrefab, transform);
        }

        // 보스 오브젝트와 스킬 풀 생성
        GameObject obj;
        bossObject = Instantiate(bossPrefab, transform);
        bossObject.SetActive(false);
        for (int i = 0; i < numOfBossSkillObj; i++)
        {
            obj = bossPrefab.GetComponent<BaseBoss>().SkillData.SkillPrefab;
            bossSkillPool[i] = Instantiate(obj, transform);
            bossSkillPool[i].SetActive(false);
        }

        // 영웅들 스킬 풀은 배틀매니저로부터
        var entryHeroes = BattleManager.Instance.GetHeroEntry();
        indexOfSkillObject = new int[numOfSkillObject];
        heroSkillPool = new GameObject[entryHeroes.Length, numOfSkillObject];
        
        for (int i = 0; i < entryHeroes.Length; i++)
        {
            obj = entryHeroes[i].prefab.GetComponent<BaseHero>().SkillData.SkillPrefab;
            for (int j = 0; j < numOfSkillObject; j++)
            {
                heroSkillPool[i, j] = Instantiate(obj, transform);
                heroSkillPool[i, j].SetActive(false);
            }
            indexOfSkillObject[i] = 0;
        }
    }

    public void InitEnemySpawn()
    {
        pos = GameObject.Find("InitEnemySpawnPosition").GetComponent<Transform>();
        Vector3 spawnPos = pos.position;
        for (int i = 0; i < normalEnemyPool.Length; i++)
        {
            spawnPos.x += 2f;
            normalEnemyPool[i].transform.position = spawnPos;
            normalEnemyPool[i].SetActive(true);
        }
    }

    public void SpawnNormalEnemy(Vector3 spawnPos)
    {
        if (bossObject.activeSelf == false)
        {
            foreach (var enemy in normalEnemyPool)
            {
                if (enemy.activeSelf == false)
                {
                    spawnPos.x += 8f; // x축을 카메라 화면 약간 밖인 위치로 이동
                    spawnPos.y = pos.position.y;
                    spawnPos.z = 0f;
                    enemy.transform.position = spawnPos;
                    enemy.SetActive(true);
                    break;
                }
            }
        }
    }

    public void SpawnBoss(Vector3 spawnPos)
    {
        if (bossObject.activeSelf == false)
        {
            spawnPos.x += 12f; // x축을 카메라 화면 약간 밖인 위치로 이동
            spawnPos.y = pos.position.y;
            spawnPos.z = 0f;
            bossObject.transform.position = spawnPos;
            bossObject.SetActive(true);
        }
    }

    public void InActiveBoss()
    {
        bossObject.SetActive(false);
    }

    public GameObject GetSkillObject(int index)
    {
        GameObject skillObj = heroSkillPool[index, indexOfSkillObject[index]];

        ++indexOfSkillObject[index];
        if (indexOfSkillObject[index] >= numOfSkillObject)
            indexOfSkillObject[index] = 0;

        skillObj.SetActive(true);
        return skillObj;
    }
}
