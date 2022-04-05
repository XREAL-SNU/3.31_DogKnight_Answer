using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float _randomHeal;

    /// <summary>
    /// 1. Init: 초기화 기능
    /// 1) Subject에 Observer로 등록
    /// 2) _myName, _myHp, _myDamage 초기화
    /// 3) _myName은 무조건 "Enemy"로 할 것
    /// 4) _myHp, _myDamage는 100, 10으로 각각 초기화 (권장 사항)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
        _myHp = 100;
        _myHpMax = _myHp;
        _myDamage = 15;
        GameManager.Instance().AddCharacter(this.GetComponent<Enemy>());
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound가 지날때마다 데미지 3씩 증가
    /// 2) _gameRound가 10이 되면 무조건 Player를 죽이도록 데미지 증가
    /// </summary>
    public override void Attack()
    {
        if (_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _myDamage += 3;
            if (_gameRound >= 10) _myDamage = GameManager.Instance().GetCharacter("Player")._myHp;
            AttackMotion();
            GameManager.Instance().GetCharacter("Player").GetHit(_myDamage);
        }
    }

    /// <summary>
    /// GetHit:
    /// 1) Player의 _randomAttack과 동일한 기능
    /// 2) 30%의 확률로 피격시 10 체력 증가
    ///   + Debug.Log($"{_myName} Heal!"); 추가
    /// </summary>
    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        if (_myHp > 0)
        {
            _randomHeal = Random.Range(0, 10);
            if (_randomHeal < 3)
            {
                StartCoroutine(HealCoroutine());
            }
        }
    }

    IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        _myHp += 10;
        Debug.Log($"{_myName} Heal!");
    }
}

