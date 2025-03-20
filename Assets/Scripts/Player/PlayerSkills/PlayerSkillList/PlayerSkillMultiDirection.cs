using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMultiDirection : PlayerSkillAbstract
{
    public int MultiDirCount = 0;
    public override void Upgrade()
    {
        base.Upgrade();
        if (MultiDirCount >= _levelSkill - 1) return;
        MultiDirCount++;
        Debug.Log("Skill Bullet Multi Direction");
    }
}
