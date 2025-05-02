using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillLightning : PlayerSkillAbstract
{
    public int TimeLightning = 9;
    [SerializeField] private HitLightning _hitLightning;
    [SerializeField] private float _timeCount;

    private void Update()
    {
        if (_levelSkill < 2 || _levelSkill > _maxLevel || !UIGamePlayManager.Ins.CheckPlayTime || PlayerCtrl.Ins.PlayerTarget.Target == null) return;
        _timeCount += Time.deltaTime;
        if (_timeCount >= TimeLightning)
        {
            _timeCount = 0;
            foreach (EnemyCtrlAbstract enemy in PlayerCtrl.Ins.PlayerTarget.ListEnemyTarget)
            {
                Observer.NotifyObserver(ObserverID.EnemyTakeDmgSingle, enemy);
                PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitLightning, enemy.transform.position, Quaternion.identity);
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeLightning <= _levelSkill) return;
        TimeLightning -= 2;

        //CancelInvoke(nameof(CheckLvSkillLightning));
        //CheckLvSkillLightning();
    }

    //private void CheckLvSkillLightning()
    //{
    //    Invoke(nameof(CheckLvSkillLightning), TimeLightning);
    //    if (_levelSkill <= 1) return;
    //    if (!UIGamePlayManager.Ins.CheckPlayTime) return;
    //    if (_playerSkillCtrl.PlayerCtrl.PlayerTarget.Target == null) return;

    //    foreach (EnemyCtrlAbstract enemy in _playerSkillCtrl.PlayerCtrl.PlayerTarget.ListEnemyTarget)
    //    {
    //        Observer.Notify(ObserverID.EnemyTakeDmgSingle, enemy);
    //        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitLightning, enemy.transform.position, Quaternion.identity);
    //    }
    //}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_hitLightning != null) return;
        _hitLightning = Resources.Load<HitLightning>("Hits/HitLightning");
    }
}
