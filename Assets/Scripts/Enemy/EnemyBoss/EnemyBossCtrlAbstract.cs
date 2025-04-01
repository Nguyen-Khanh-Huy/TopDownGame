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

    public void EventOnAttackDash()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackDash = true;
        _enemyAttack.GetComponent<EnemyBossAttack>().IsSpawnVfxDash = false;
    }
    public void EventOffAttackDash()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackDash = false;
        _enemyAttack.GetComponent<EnemyBossAttack>().StopAttackDash();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackRain()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackRain = true;
    }
    public void EventOFFAttackRain()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackRain = false;
        _enemyAttack.GetComponent<EnemyBossAttack>().ResetInforAttackRain();
    }
}
