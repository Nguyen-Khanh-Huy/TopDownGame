using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBossCtrlAbstract : EnemyCtrlAbstract
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyBossSO");
    }

    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        base.OnEnable();
    }

    public void EventAttackDash()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsDash = true;
    }
}
