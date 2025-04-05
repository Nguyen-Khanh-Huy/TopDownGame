using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSO", menuName = "PIS/PlayerSkillSO")]

public class PlayerSkillSO : ScriptableObject
{
    public int LevelSkill;
    public int MaxLevel;

    [Header("Infor Skills:")]
    public float MoveSpeed;
    public float ShootRange;
    public float ShootSpeed;
    public int MultiShotCount;
    public int MultiDirCount;
    public int AoeRange;
    public int TimeLightning;
    public int SpinBallCount;
    public int TimeFreeze;
}
