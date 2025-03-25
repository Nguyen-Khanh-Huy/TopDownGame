using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillLightning : PlayerSkillAbstract
{
    public int TimeLightning = 7;
    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeLightning <= _levelSkill) return;
        TimeLightning -= 2;
    }
}
