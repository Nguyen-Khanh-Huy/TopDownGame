using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFiveShots : PlayerSkillAbstract
{
    public int ShotCount = 1;
    public override void Upgrade()
    {
        base.Upgrade();
        ShotCount++;
        Debug.Log("Skill Bullet Three Time");
    }
}
