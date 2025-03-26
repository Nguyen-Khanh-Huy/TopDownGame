using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillLightning : PlayerSkillAbstract
{
    public int TimeLightning = 7;
    [SerializeField] private HitLightning _hitLightning;

    private void Start()
    {
        //Invoke(nameof(CheckLvSkillLightning), _playerSkillCtrl.PlayerSkillList.PlayerSkillLightning.TimeLightning);
        CheckLvSkillLightning();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeLightning <= _levelSkill) return;
        TimeLightning -= 2;
    }

    private void CheckLvSkillLightning()
    {
        Invoke(nameof(CheckLvSkillLightning), TimeLightning);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (_levelSkill <= 1) return;
        if (_playerSkillCtrl.PlayerCtrl.PlayerTarget.Target == null) return;

        foreach (EnemyCtrlAbstract enemy in _playerSkillCtrl.PlayerCtrl.PlayerTarget.ListEnemyTarget)
        {
            Observer.Notify(ObserverID.EnemyTakeDmg, enemy);
            PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitLightning, enemy.transform.position, Quaternion.identity);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_hitLightning != null) return;
        _hitLightning = Resources.Load<HitLightning>("Hits/HitLightning");
    }
}
