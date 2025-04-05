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