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
        float SO = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.ShootRange;
        if (_levelSkill == 2)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.10f);
            ChangePlayerSpriteAttackRange();
        }
        else if (_levelSkill == 3)
        {
            _playerSkillCtrl.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.20f);
            ChangePlayerSpriteAttackRange();
        }
        CheckLvSkillSpinBall();
    }

    private void ChangePlayerSpriteAttackRange()
    {
        _playerSkillCtrl.PlayerCtrl.PlayerSpriteAttackRange.transform.localScale =
                new Vector3(_playerSkillCtrl.PlayerCtrl.PlayerTarget.PlayerCollider.radius * 1.5f,
                            _playerSkillCtrl.PlayerCtrl.PlayerTarget.PlayerCollider.radius * 1.5f, 1);
    }

    private void CheckLvSkillSpinBall()
    {
        if (_playerSkillCtrl.PlayerSkillSpinBall.LevelSkill > 1)
            _playerSkillCtrl.PlayerSkillSpinBall.UpdateBallPos();
    }
}