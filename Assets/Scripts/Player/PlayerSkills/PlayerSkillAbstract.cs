using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected int _levelSkill;

    public int LevelSkill { get => _levelSkill; }

    public abstract void LaunchSkill();

    protected virtual void Start()
    {
        _levelSkill = 1;
    }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }
}
