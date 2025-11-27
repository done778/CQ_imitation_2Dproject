using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HeroPosition
{
    Blue, Red, Green, End
}

// 스킬 블록과 영웅을 이어주는 역할
public class BattleManager : SingletonePattern <BattleManager>
{
    // 얘를 싱글톤으로 해야할까?

    // 얘는 스테이지 씬에서만 필요하므로 싱글톤으로 안하고
    // 스테이지 진입 시 게임 매니저가 로비에서 가져온 3명의 영웅 정보를 가져옴.

    // 또는 싱글톤으로 구현해서 로비에서 고른 영웅의 정보를 그냥 얘가 가지고 있게 해도 될 것 같다.

    // 3명의 영웅을 1,2,3번 자리에 배치 시키기.
    // 왜냐하면 스킬 블록 타입과 매칭이 되어야하기 때문
    // 영웅 스킬 시전 명령도 얘를 통해서 하도록 한다.

    // 그리고 플레이어와 적이 직접 서로 메서드를 호출하지 않고 얘를 통해서 상호작용하도록 한다.

    private BaseHero[] heroEntry;
    private CharacterBaseStatus[] entryHeroId;
    private int maxPartyHealthPoint;
    private int curPartyHealthPoint;
    private CameraController virtualCam;
    private PoolManager poolManager;
    private int enemyKillCount;
    private int goalKillCount;

    public event Action<int, int> changePlayerHp;
    public event Action<int, int> changeProgress;

    public Dictionary<int, float> chainBonus;

    protected override void Awake()
    {
        base.Awake();
        enemyKillCount = 0;
        goalKillCount = 10;

        heroEntry = new BaseHero[3];
        entryHeroId = new CharacterBaseStatus[3]; // 스크립터블 오브젝트 안에 영웅 정보가 있음.

        chainBonus = new Dictionary<int, float>();

        chainBonus[1] = 1f;
        chainBonus[2] = 2.2f;
        chainBonus[3] = 4.5f;

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // 영웅 엔트리 설정 메서드
    // 매개변수 : 어느 자리에(index), 누구(hero)를 넣을 것인가? 
    public void SetHeroEntry(int index, CharacterBaseStatus hero)
    {
        entryHeroId[index] = hero;
    }

    // 히어로 엔트리 ID에 대한 정보 반환 : 스테이지 씬 진입시 모든 자리에 영웅이 배치되었나 확인용
    public CharacterBaseStatus[] GetHeroEntry()
    {
        return entryHeroId;
    }

    private void InitHeroEntry()
    {
        for (int i = 0; i < heroEntry.Length; i++)
        {
            heroEntry[i] = null;
            entryHeroId[i] = null;
        }
    }

    // 스테이지 씬 들어갔을 때 실행될 메서드
    private void GameStart()
    {
        enemyKillCount = 0;
        // 정해진 위치에 영웅들 인스턴스화 하고 정보도 담음.
        string[] positionNames = { "FrontPosition", "MiddlePosition", "BackPosition" };
        for (int i = 0; i < entryHeroId.Length; i++)
        {
            Transform pos = GameObject.Find(positionNames[i]).GetComponent<Transform>();
            GameObject curHero = Instantiate(entryHeroId[i].prefab, pos.transform.position, pos.transform.rotation);
            heroEntry[i] = curHero.GetComponent<BaseHero>();
        }

        virtualCam.CameraInit(heroEntry[0].gameObject);

        // 출전한 영웅 3명의 체력 합산
        maxPartyHealthPoint = 0;
        foreach (var hero in heroEntry)
        {
            maxPartyHealthPoint += hero.status.HealthPoint;
        }
        curPartyHealthPoint = maxPartyHealthPoint;
        changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
        changeProgress?.Invoke(enemyKillCount, goalKillCount);

        poolManager.InitEnemySpawn();
    }

    public void CastingSkill(SkillBlock block)
    {
        switch (block.type)
        {
            case BlockType.Blue:
                heroEntry[(int)HeroPosition.Blue]?.RegistSkillQueue(block.numChain);
                break;
            case BlockType.Red:
                heroEntry[(int)HeroPosition.Red]?.RegistSkillQueue(block.numChain);
                break;
            case BlockType.Green:
                heroEntry[(int)HeroPosition.Green]?.RegistSkillQueue(block.numChain);
                break;
            default:
                Debug.LogError("Input wrong SkillBlock type");
                break;
        }
    }

    public void BaseAttackInteraction(BaseCharacter Attacker, BaseCharacter target)
    {
        if (target.CompareTag("Player"))
        {
            curPartyHealthPoint -= Attacker.status.AttackPower;
            changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
            AudioManager.Instance.PlayHeroHitSound();
        }
        else
        {
            if (target is EnemyController)
            {
                (target as EnemyController).TakeDamage(Attacker.status.AttackPower);
            }
        }
        if (curPartyHealthPoint <= 0) 
        {
            foreach (var hero in heroEntry)
            {
                hero.Died();
            }
            // 영웅이 죽으면 게임매니저에게 끝났다고 알림.
            GameManager.Instance.FailedGame();
        }
    }

    public GameObject RequestSkillObj(int id)
    {
        for (int i = 0; i < heroEntry.Length; i++)
        {
            if (id == heroEntry[i].status.UniqueId)
            {
                return poolManager.GetSkillObject(i);
            }
        }
        return null;
    }

    public void Healing(int amount)
    {
        curPartyHealthPoint += amount;
        if (curPartyHealthPoint > maxPartyHealthPoint) 
        {
            curPartyHealthPoint = maxPartyHealthPoint;
        }
        changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
    }

    public void SkillAttackInteraction(BaseCharacter target, int damage)
    {
        if (target.CompareTag("Enemy"))
        {
            if (target is EnemyController)
            {
                (target as EnemyController).TakeDamage(damage);
            }
        }
    }

    // 일반 적이 죽으면 파괴가 아닌 비활성화를 시켜야한다.
    // 하지만 비활성화해도 영웅들의 타겟에서는 사라지지 않는다.
    // 그러므로 영웅들의 현재 타겟을 제거해줘서 다시 탐색하도록 한다.
    public void NormalEnemyDied(GameObject enemy)
    {
        // 일단 비활성화, 하지만 타겟에선 사라지지 않음
        enemy.SetActive(false);
        foreach (var hero in heroEntry)
        {
            if(hero.targetCombat != null && hero.targetCombat.gameObject == enemy)
            {
                hero.targetCombat = null;
            }
        }
        // 타겟에서 제거했으니 킬 카운트에 따라 풀 매니저에게 소환 명령
        enemyKillCount++;
        changeProgress?.Invoke(enemyKillCount, goalKillCount);
        if (enemyKillCount >= goalKillCount)
            poolManager.SpawnBoss(virtualCam.transform.position);
        else
            poolManager.SpawnNormalEnemy(virtualCam.transform.position);
    }

    public void BossDied()
    {
        poolManager.InActiveBoss();
        GameManager.Instance.ClearGame();
    }

    public void RegistPoolManager(PoolManager manager)
    {
        poolManager = manager;
    }
    public void RegistVirtualCamera(CameraController controller)
    {
        virtualCam = controller;
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Stage"))
        {
            GameStart();
        }
        if (scene.name == "Lobby")
        {
            InitHeroEntry();
        }
    }
}
