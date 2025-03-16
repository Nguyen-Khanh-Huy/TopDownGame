using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMultiDirection : PlayerSkillAbstract
{
    public int TripleBeam = 0;
    public override void Upgrade()
    {
        base.Upgrade();
        TripleBeam++;
        Debug.Log("Skill Bullet Triple Beam");
    }
}
