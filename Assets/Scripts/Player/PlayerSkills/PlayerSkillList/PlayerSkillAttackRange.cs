using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillAttackRange : PlayerSkillAbstract
{
    public override void Upgrade()
    {
        base.Upgrade();
        Debug.Log("Skill Index Attack Range");
    }
}