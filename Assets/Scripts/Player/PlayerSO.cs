using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "PIS/PlayerSO")]

public class PlayerSO : ScriptableObject
{
    public int Hp;

    public int CurMana;
    public int CurLevel;
    public int ManaNextLevel;
    public int CountEnemyDead;
}
