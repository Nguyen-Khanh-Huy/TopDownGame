using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerSkillsCtrl _playerSkillCtrl;
    [SerializeField] protected int _levelSkill;
    [SerializeField] protected int _maxLevel;

    public int LevelSkill { get => _levelSkill; }

    private void OnEnable()
    {
        Init();
    }

    public virtual void Upgrade()
    {
        if (_levelSkill <= 0 || _levelSkill >= _maxLevel) return;
        _levelSkill++;
        // For Override
    }

    private void Init()
    {
        _levelSkill = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.LevelSkill;
        _maxLevel = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MaxLevel;

        _playerSkillCtrl.PlayerSkillMultiShot.MultiShotCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MultiShotCount;
        _playerSkillCtrl.PlayerSkillMultiDirection.MultiDirCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MultiDirCount;
        _playerSkillCtrl.PlayerSkillAoeDamage.AoeRange = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.AoeRange;
        _playerSkillCtrl.PlayerSkillLightning.TimeLightning = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.TimeLightning;
        _playerSkillCtrl.PlayerSkillSpinBall.SpinBallCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.SpinBallCount;
        _playerSkillCtrl.PlayerSkillFreeze.TimeFreeze = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.TimeFreeze;
    }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
    }
}
