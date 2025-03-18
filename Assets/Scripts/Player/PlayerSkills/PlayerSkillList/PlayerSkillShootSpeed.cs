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

    public void CheckLevelAttackSpeed()
    {
        if (_levelSkill < 2 || _levelSkill > 3) return;
        float SO = _playerSkillCtr.PlayerCtrl.PlayerSO.ShootSpeed;
        if (_levelSkill == 2)
        {
            Debug.Log("Skill Index Attack Speed LV2");
            _playerSkillCtr.PlayerCtrl.PlayerShoot.ShootSpeed = SO - (SO * 0.15f);
        }
        else if (_levelSkill == 3)
        {
            Debug.Log("Skill Index Attack Speed LV3");
            _playerSkillCtr.PlayerCtrl.PlayerShoot.ShootSpeed = SO - (SO * 0.25f);
        }
    }
}
