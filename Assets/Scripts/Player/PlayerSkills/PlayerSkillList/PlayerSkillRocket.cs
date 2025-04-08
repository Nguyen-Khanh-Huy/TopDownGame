using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillRocket : PlayerSkillAbstract
{
    public int TimeRoket = 8;

    private void Start()
    {
        CheckLvSkillRocket();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeRoket <= 4) return;
        TimeRoket -= 2;
    }

    private void CheckLvSkillRocket()
    {
        Invoke(nameof(CheckLvSkillRocket), TimeRoket);

    }
}
