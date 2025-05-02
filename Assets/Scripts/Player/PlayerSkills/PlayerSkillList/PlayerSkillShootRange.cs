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
        float SO = PlayerCtrl.Ins.PlayerSkillSO.ShootRange;
        if (_levelSkill == 2)
        {
            PlayerCtrl.Ins.PlayerTarget.ColliderTarget.radius = SO + (SO * 0.10f);
            ChangePlayerSpriteAttackRange();
        }
        else if (_levelSkill == 3)
        {
            PlayerCtrl.Ins.PlayerTarget.ColliderTarget.radius = SO + (SO * 0.20f);
            ChangePlayerSpriteAttackRange();
        }
        CheckLvSkillSpinBall();
    }

    private void ChangePlayerSpriteAttackRange()
    {
        PlayerCtrl.Ins.PlayerSpriteAttackRange.transform.localScale =
                new Vector3(PlayerCtrl.Ins.PlayerTarget.ColliderTarget.radius * 1.5f,
                            PlayerCtrl.Ins.PlayerTarget.ColliderTarget.radius * 1.5f, 1);
    }

    private void CheckLvSkillSpinBall()
    {
        if (_playerSkillCtrl.PlayerSkillSpinBall.LevelSkill > 1)
            _playerSkillCtrl.PlayerSkillSpinBall.UpdateBallPos();
    }
}