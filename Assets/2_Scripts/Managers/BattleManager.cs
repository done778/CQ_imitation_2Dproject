using System;
using UnityEngine;

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
    private int maxPartyHealthPoint;
    private int curPartyHealthPoint;

    public event Action<int, int> changePlayerHp;

    protected override void Awake()
    {
        base.Awake();
        heroEntry = new BaseHero[3];
    }

    // 스테이지 씬 들어갔을 때 실행될 메서드
    public void StageSceneInit()
    {
        maxPartyHealthPoint = 0;
        foreach (var hero in heroEntry) {
            maxPartyHealthPoint += hero.status.HealthPoint;
        }
        curPartyHealthPoint = maxPartyHealthPoint;
        changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
    }

    // 영웅 엔트리 설정 메서드
    public void SetHeroEntry()
    {

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
        }
        else
        {
            if (target is EnemyController)
            {
                (target as EnemyController).TakeDamage(Attacker.status.AttackPower);
            }
        }
        Debug.Log($"{Attacker.name}이(가) {target.name}을 공격!");
        if (curPartyHealthPoint <= 0) 
        {
            foreach (var hero in heroEntry)
            {
                hero.Died();
            }
        }
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
}
