using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroPosition
{
    Blue, Red, Green, End
}

// 스킬 블록과 영웅을 이어주는 역할
public class BattleManager : SingletonePattern <BattleManager>
{
    // 얘는 스테이지 씬에서만 필요하므로 싱글톤 X
    // 스테이지 진입 시 게임 매니저가 로비에서 가져온 3명의 영웅 정보를 가져옴.

    // 또는 싱글톤으로 구현해서 로비에서 고른 영웅의 정보를 그냥 얘가 가지고 있게 함.

    // 3명의 영웅을 1,2,3번째 자리에 배치 시켜야 함.
    // 왜냐하면 스킬 블록 타입과 매칭이 되어야하기 때문

    [SerializeField] private BaseHero[] heroEntry;
    private int maxPartyHealthPoint;
    private int curPartyHealthPoint;

    public event Action<int, int> changePlayerHp;

    public void Start()
    {
        maxPartyHealthPoint = 0;
        //heroEntry = new BaseHero[3];
        foreach (var hero in heroEntry) {
            maxPartyHealthPoint += hero.status.HealthPoint;
        }
        curPartyHealthPoint = maxPartyHealthPoint;
        changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
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
        if (Attacker.CompareTag("Enemy"))
        {
            curPartyHealthPoint -= Attacker.status.AttackPower;
            changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
        }
        else if (target.CompareTag("Boss"))
        {
            // 보스 체력으로 수정할 것
            curPartyHealthPoint -= Attacker.status.AttackPower;
            changePlayerHp?.Invoke(curPartyHealthPoint, maxPartyHealthPoint);
        }
        else
        {
            if (target is NormalEnemyController)
            {
                (target as NormalEnemyController).TakeDamage(Attacker.status.AttackPower);
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
}
