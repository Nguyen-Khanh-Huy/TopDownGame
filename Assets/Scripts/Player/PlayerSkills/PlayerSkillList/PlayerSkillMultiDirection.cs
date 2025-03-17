using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMultiDirection : PlayerSkillAbstract
{
    public int DirCount = 0;
    public override void Upgrade()
    {
        base.Upgrade();
        if (DirCount >= _levelSkill - 1) return;
        DirCount++;
        Debug.Log("Skill Bullet Multi Direction");
    }
}
