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
        float SO = PlayerCtrl.Ins.PlayerSkillSO.ShootSpeed;

        if (_levelSkill == 2)
            PlayerCtrl.Ins.PlayerShoot.ShootSpeed = SO - (SO * 0.15f);
        else if (_levelSkill == 3)
            PlayerCtrl.Ins.PlayerShoot.ShootSpeed = SO - (SO * 0.25f);
    }
}
