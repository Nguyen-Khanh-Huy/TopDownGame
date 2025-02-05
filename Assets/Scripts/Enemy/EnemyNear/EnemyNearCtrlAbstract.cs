using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyNearCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyNearAttack _enemyNearAttack;
    [SerializeField] private SphereCollider _colliderAttack;
    public EnemyNearAttack EnemyNearAttack { get => _enemyNearAttack; set => _enemyNearAttack = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null && _enemyNearAttack != null && _colliderAttack != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyNearSO");
        _enemyNearAttack = GetComponentInChildren<EnemyNearAttack>();
        _colliderAttack = GetComponentInChildren<SphereCollider>();
    }

    private void OnEnable()
    {
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
    }

    private void OnDisable()
    {
        EnemyManager.Ins.ListEnemyNearSpawn.Remove(this);
    }

    public void EventOnColliderAttack()
    {
        _colliderAttack.enabled = true;
    }

    public void EventOffColliderAttack()
    {
        _colliderAttack.enabled = false;
    }
}
