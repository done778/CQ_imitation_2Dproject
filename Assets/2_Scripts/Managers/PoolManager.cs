using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    private int numOfNormalEnemy = 4;
    private GameObject[] normalEnemyPool;
    private GameObject bossObject;
    private Transform pos;

    private void Awake()
    {
        BattleManager.Instance.RegistPoolManager(this);
        normalEnemyPool = new GameObject[numOfNormalEnemy];
        for (int i = 0; i < normalEnemyPool.Length; i++)
        {
            normalEnemyPool[i] = Instantiate(normalEnemyPrefab, transform);
        }
        bossObject = Instantiate(bossPrefab, transform);
        bossObject.SetActive(false);
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
}
