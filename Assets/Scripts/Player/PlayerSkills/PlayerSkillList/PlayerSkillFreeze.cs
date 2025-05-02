using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFreeze : PlayerSkillAbstract
{
    public int TimeFreeze = 0;
    [SerializeField] private float _coolDown = 6f;
    private float _timeCount;

    private void Update()
    {
        if (_levelSkill < 2 || _levelSkill > _maxLevel || !UIGamePlayManager.Ins.CheckPlayTime || PlayerCtrl.Ins.PlayerTarget.Target == null) return;
        _timeCount += Time.deltaTime;
        if (_timeCount >= _coolDown)
        {
            _timeCount = 0;
            foreach (EnemyCtrlAbstract enemy in PlayerCtrl.Ins.PlayerTarget.ListEnemyTarget)
            {
                enemy.EnemyMoving.IsFreeze = true;
                enemy.EnemyFlashingEffect.StartFreeze();
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeFreeze >= _levelSkill - 1) return;
        TimeFreeze++;

        //CancelInvoke(nameof(CheckLvSkillFreeze));
        //CheckLvSkillFreeze();
    }

    //private void CheckLvSkillFreeze()
    //{
    //    Invoke(nameof(CheckLvSkillFreeze), _coolDown);
    //    if (_levelSkill <= 1) return;
    //    if (!UIGamePlayManager.Ins.CheckPlayTime) return;
    //    if (_playerSkillCtrl.PlayerCtrl.PlayerTarget.Target == null) return;

    //    foreach (EnemyCtrlAbstract enemy in _playerSkillCtrl.PlayerCtrl.PlayerTarget.ListEnemyTarget)
    //    {
    //        enemy.EnemyMoving.IsFreeze = true;
    //        enemy.EnemyFlashingEffect.StartFreeze();
    //    }
    //}
}
