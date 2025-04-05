using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillAoeDamage : PlayerSkillAbstract
{
    public int AoeRange = 0;
    public override void Upgrade()
    {
        base.Upgrade();
        if (AoeRange >= _levelSkill - 1) return;
        AoeRange++;
    }
}
