using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMultiShot : PlayerSkillAbstract
{
    public int MultiShotCount = 1;
    public override void Upgrade()
    {
        base.Upgrade();
        if (MultiShotCount >= _levelSkill) return;
        MultiShotCount++;
    }
}
