using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFreeze : PlayerSkillAbstract
{
    public int TimeFreeze = 0;
    private float _coolDown = 6f;

    private void Start()
    {
        CheckLvSkillFreeze();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeFreeze >= _levelSkill - 1) return;
        TimeFreeze++;
    }
    private void CheckLvSkillFreeze()
    {
        Invoke(nameof(CheckLvSkillFreeze), _coolDown);
        if (_levelSkill <= 1) return;
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (_playerSkillCtrl.PlayerCtrl.PlayerTarget.Target == null) return;

        foreach (EnemyCtrlAbstract enemy in _playerSkillCtrl.PlayerCtrl.PlayerTarget.ListEnemyTarget)
        {
            enemy.EnemyMoving.IsFreeze = true;
        }
    }
}
