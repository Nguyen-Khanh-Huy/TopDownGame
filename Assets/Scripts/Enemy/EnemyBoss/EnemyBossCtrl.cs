using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossCtrl : EnemyCtrlAbstract
{
    private EnemyBossAttack EnemyBossAttack => _enemyAttack as EnemyBossAttack;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO == null)
            _enemySO = Resources.Load<EnemySO>("SO/EnemyBossSO");
    }

    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        base.OnEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
        }
    }

    //------------------------------------------------------------------------

    public void EventOnAttackDash()
    {
        EnemyBossAttack.IsAttackDash = true;
    }
    public void EventOffAttackDash()
    {
        EnemyBossAttack.IsAttackDash = false;
        EnemyBossAttack.StopAttackDash();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackRain()
    {
        EnemyBossAttack.IsAttackRain = true;
    }
    public void EventOffAttackRain()
    {
        EnemyBossAttack.IsAttackRain = false;
        EnemyBossAttack.ResetInforAttackRain();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackLaser()
    {
        EnemyBossAttack.IsAttackLaser = true;
    }
    public void EventOffAttackLaser()
    {
        EnemyBossAttack.IsAttackLaser = false;
        EnemyBossAttack.StopAttackLaser();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackFire()
    {
        EnemyBossAttack.IsAttackFire = true;
    }

    //------------------------------------------------------------------------

    public override string GetName()
    {
        return "EnemyBoss";
    }
}
