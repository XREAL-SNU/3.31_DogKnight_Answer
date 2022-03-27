using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character: MonoBehaviour, Observer
{
    protected Animator _animator;

    public string _myName;
    public float _myHp;
    public float _myDamage;
    public int _playerNumber;

    protected int _gameRound;
    protected int _whoseTurn;
    protected bool _isFinised;

    public void ObserverTurnUpdate(int round, int turn)
    {
        _gameRound = round;
        _whoseTurn = turn;
    }

    public void ObserverFinishUpdate(bool isFinish)
    {
        _isFinised = isFinish;
    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Attack()
    {
        
    }

    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
    }

    public void AttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsAttack.ToString());
    }
    public void SpecialAttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsSpecialAttack.ToString());
    }

    public void DeadMotion()
    {
        StartCoroutine(DeadCoroutine());
    }

    public void GetHitMotion()
    {
        StartCoroutine(GetHitCoroutine());
    }

    IEnumerator GetHitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.GetHit.ToString());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
    }
}