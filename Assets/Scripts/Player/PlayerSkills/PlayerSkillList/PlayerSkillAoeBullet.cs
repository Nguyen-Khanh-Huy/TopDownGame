using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillAoeBullet : PlayerSkillAbstract
{
    public int AoeBullet = 0;
    public override void Upgrade()
    {
        base.Upgrade();
        CheckLevelAoeBullet();
    }

    public void CheckLevelAoeBullet()
    {
        if (_levelSkill < 2 || _levelSkill > 3) return;
        if (_levelSkill == 2)
        {
            AoeBullet = 2;
        }
        else if (_levelSkill == 3)
        {
            AoeBullet = 3;
        }
    }

}
