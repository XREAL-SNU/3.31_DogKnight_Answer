using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;

    protected override void Init()
    {
        base.Init();
        PlayerManager.Instance().AddCharacter(this.GetComponent<Enemy>());
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 15;
        _playerNumber = 1;
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        //Init();
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Debug.Log($"{_player._myName} In");
        }
    }
    public override void Attack()
    {
        if (this._playerNumber.Equals(_whoseTurn) && !_isFinised)
        {
            base.Attack();
            AttackMotion();
            _player.GetHit(this._myDamage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        if (_myHp <= 0)
        {
            DeadMotion();
            PlayerManager.Instance().EndNotify(_player._myName);
        }
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}
