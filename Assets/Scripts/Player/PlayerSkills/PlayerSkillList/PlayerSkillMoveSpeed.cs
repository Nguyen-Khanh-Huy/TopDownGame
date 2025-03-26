using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMoveSpeed : PlayerSkillAbstract
{
    public override void Upgrade()
    {
        base.Upgrade();
        CheckLevelMoveSpeed();
    }

    public void CheckLevelMoveSpeed()
    {
        if (_levelSkill < 2 || _levelSkill > 3) return;
        float SO = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MoveSpeed;
        if (_levelSkill == 2)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerMoving.MoveSpeed = SO + (SO * 0.15f);
        }
        else if (_levelSkill == 3)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerMoving.MoveSpeed = SO + (SO * 0.3f);
        }
    }
}