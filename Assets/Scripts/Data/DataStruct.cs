using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk
}

public enum EnemyCreepState
{
    Walk,
    Hit,
    Dying
}

public enum EnemyNearLongState
{
    Idle,
    Walk,
    Attack,
    Dying
}

public enum EnemyBossState
{

    Idle,
    Walk,
    AttackDash,
    AttackRain,
    AttackLaser,
    AttackFire,
    Dying
}

public enum ObserverID
{
    PlayerTakeDmg,
    EnemyTakeDmg,
    EnemyTakeDmgSingle
}