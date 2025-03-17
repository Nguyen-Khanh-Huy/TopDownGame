using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFiveShots : PlayerSkillAbstract
{
    public int ShotCount = 1;
    public override void Upgrade()
    {
        base.Upgrade();
        if (ShotCount >= _levelSkill) return;
        ShotCount++;
        Debug.Log("Skill Bullet Five Shots");
    }
}
