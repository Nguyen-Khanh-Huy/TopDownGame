using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSO", menuName = "PIS/PlayerSkillSO")]

public class PlayerSkillSO : ScriptableObject
{
    public int LevelSkill;
    public int MaxLevel;
    public int ShotCount;
    public int DirCount;
    public int AoeBullet;
}
