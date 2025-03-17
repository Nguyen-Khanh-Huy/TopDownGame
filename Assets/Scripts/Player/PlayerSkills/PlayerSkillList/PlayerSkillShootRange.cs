using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillShootRange : PlayerSkillAbstract
{
    public override void Upgrade()
    {
        base.Upgrade();
        CheckLevelShootRange();
    }

    public void CheckLevelShootRange()
    {
        if (_levelSkill < 2 || _levelSkill > 3) return;
        float SO = _playerSkillCtr.PlayerCtrl.PlayerSO.ShootRange;
        if (_levelSkill == 2)
        {
            Debug.Log("Skill Index Shoot Range LV2");
            _playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.10f);
        }
        else if (_levelSkill == 3)
        {
            Debug.Log("Skill Index Shoot Range LV3");
            _playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.20f);
        }
    }
}