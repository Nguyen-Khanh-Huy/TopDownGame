using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyNearCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private SphereCollider _colliderAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null && _colliderAttack != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyNearSO");
        _colliderAttack = GetComponentInChildren<SphereCollider>();
    }

    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        EnemyManager.Ins.ListEnemyNearSpawn.Remove(this);
        base.OnDisable();
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
