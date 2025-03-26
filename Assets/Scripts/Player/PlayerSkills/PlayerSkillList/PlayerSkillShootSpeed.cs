using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillShootSpeed : PlayerSkillAbstract
{
    public override void Upgrade()
    {
        base.Upgrade();
        CheckLevelAttackSpeed();
    }

    private void CheckLevelAttackSpeed()
    {
        if (_levelSkill < 2 || _levelSkill > 3) return;
        float SO = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.ShootSpeed;
        if (_levelSkill == 2)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerShoot.ShootSpeed = SO - (SO * 0.15f);
        }
        else if (_levelSkill == 3)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerShoot.ShootSpeed = SO - (SO * 0.25f);
        }
    }
}
