using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _attackChoose;

    protected override void Init()
    {
        base.Init();
        PlayerManager.Instance().AddCharacter(this.GetComponent<Player>());
        _myName = "Player";
        _myHp = 100;
        _myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        if(_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
            Debug.Log($"{_enemy._myName} In");
        }
    }

    public override void Attack()
    {
        if (this._myName.Equals(_whoseTurn) && !_isFinised)
        {
            _attackChoose = Random.Range(0, 10);
            if (_attackChoose < 7)
            {
                AttackMotion();
                _enemy.GetHit(this._myDamage);
            }
            else
            {
                SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                _enemy.GetHit(this._myDamage + 10);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        if (_myHp <= 0)
        {
            DeadMotion();
            PlayerManager.Instance().EndNotify(_enemy._myName);
        }
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}
