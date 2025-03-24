using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk
}

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Dying
}

public enum ObserverID
{
    PlayerTakeDmg,
    EnemyTakeDmg,
    PlayerSkill1,
    PlayerSkill2,
    PlayerSkill3,
    PlayerSkill4,
    PlayerSkill5
}