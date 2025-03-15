using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player SO", menuName = "PIS/Player SO")]

public class PlayerSO : ScriptableObject
{
    public int Hp;
    public float MoveSpeed;
    public float FireSpeed;
}
