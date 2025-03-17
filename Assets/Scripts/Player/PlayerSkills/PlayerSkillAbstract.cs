using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerSkillsCtrl _playerSkillCtr;
    [SerializeField] protected int _levelSkill;
    [SerializeField] protected int _maxLevel;

    public int LevelSkill { get => _levelSkill; }

    public virtual void Upgrade()
    {
        if (_levelSkill >= _maxLevel) return;
        _levelSkill++;
        // For Override
    }


    protected override void LoadComponents()
    {
        if (_playerSkillCtr != null) return;
        _playerSkillCtr = GetComponentInParent<PlayerSkillsCtrl>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _levelSkill = _playerSkillCtr.PlayerCtrl.PlayerSkillSO.LevelSkill;
        _maxLevel = _playerSkillCtr.PlayerCtrl.PlayerSkillSO.MaxLevel;
        _playerSkillCtr.PlayerSkillList.PlayerSkillFiveShots.ShotCount = _playerSkillCtr.PlayerCtrl.PlayerSkillSO.ShotCount;
        _playerSkillCtr.PlayerSkillList.PlayerSkillMultiDirection.DirCount = _playerSkillCtr.PlayerCtrl.PlayerSkillSO.DirCount;
    }
}
