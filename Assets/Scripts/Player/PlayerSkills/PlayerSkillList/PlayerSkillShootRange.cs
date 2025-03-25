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
        float SO = _playerSkillCtr.PlayerCtrl.PlayerSkillSO.ShootRange;
        if (_levelSkill == 2)
        {
            _playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.10f);
            ChangePlayerSpriteAttackRange();
        }
        else if (_levelSkill == 3)
        {
            _playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius = SO + (SO * 0.20f);
            ChangePlayerSpriteAttackRange();
        }
    }

    private void ChangePlayerSpriteAttackRange()
    {
        _playerSkillCtr.PlayerCtrl.PlayerSpriteAttackRange.transform.localScale =
                new Vector3(_playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius * 1.5f,
                            _playerSkillCtr.PlayerCtrl.PlayerTarget.PlayerCollider.radius * 1.5f, 1);
    }
}