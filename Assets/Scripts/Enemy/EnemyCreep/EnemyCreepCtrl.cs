using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreepCtrl : EnemyCtrlAbstract
{
    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        _agent.speed = _enemySO.MoveSpeed;
        base.OnEnable();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyCreepSO");
    }

    public override string GetName()
    {
        return "EnemyCreep";
    }
}
