using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSO", menuName = "PIS/PlayerSkillSO")]

public class PlayerSkillSO : ScriptableObject
{
    public int LevelSkill;
    public int MaxLevel;

    public float MoveSpeed;
    public float ShootRange;
    public float ShootSpeed;
    public int MultiShotCount;
    public int MultiDirCount;
    public int AoeBullet;
}
