using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected int _levelSkill = 1;
    [SerializeField] protected int _maxLevel = 3;

    public int LevelSkill { get => _levelSkill; }

    public virtual void SkillBullet(){}

    public virtual void SkillIndex(){}


    public virtual void Upgrade()
    {
        if (_levelSkill >= _maxLevel) return;
        _levelSkill++;
    }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }
}
