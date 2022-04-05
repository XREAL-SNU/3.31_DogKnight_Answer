using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float _randomAttack;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Player";
        _myHp = 100;
        _myHpMax = _myHp;
        _myDamage = 20;
        GameManager.Instance().AddCharacter(this.GetComponent<Player>());
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Attack:
    /// 1) Player�� 30%�� Ȯ���� ���ݷ��� �� ���� ������ ���� ��
    /// 2) _randomAttack = Random.Range(0,10); ���� ���� ���� ����
    ///   -> 0~9 ������ ���� �� �ϳ��� �������� �Ҵ����.
    /// 3) _randomAttack �̿��ؼ� 30% Ȯ���� ���� ���ݷº��� 10 ���� ���� ����
    /// 4) �̶��� AttackMotion() ���� SpecialAttackMotion() ȣ���� ��
    ///    + Debug.Log($"{_myName} Special Attack!"); �߰�
    /// 5) 70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
    /// </summary>
    public override void Attack()
    {
        if (_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _randomAttack = Random.Range(0, 10);
            if (_randomAttack < 7)
            {
                AttackMotion();
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage);
            }
            else
            {
                SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage + 10);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
