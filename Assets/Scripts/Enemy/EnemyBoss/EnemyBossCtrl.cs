using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossCtrl : EnemyCtrlAbstract
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
        }
    }

    //------------------------------------------------------------------------

    public void EventOnAttackDash()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackDash = true;
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
    public void EventOffAttackRain()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackRain = false;
        _enemyAttack.GetComponent<EnemyBossAttack>().ResetInforAttackRain();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackLaser()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackLaser = true;
    }
    public void EventOffAttackLaser()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackLaser = false;
        _enemyAttack.GetComponent<EnemyBossAttack>().StopAttackLaser();
    }

    //------------------------------------------------------------------------

    public void EventOnAttackFire()
    {
        _enemyAttack.GetComponent<EnemyBossAttack>().IsAttackFire = true;
    }

    //------------------------------------------------------------------------

    public override string GetName()
    {
        return "EnemyBoss";
    }
}
