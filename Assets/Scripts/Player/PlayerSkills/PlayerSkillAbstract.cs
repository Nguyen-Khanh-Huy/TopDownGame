using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerSkillsCtrl _playerSkillCtrl;
    [SerializeField] protected int _levelSkill;
    [SerializeField] protected int _maxLevel;

    public int LevelSkill { get => _levelSkill; }

    private void Start()
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
        _levelSkill = PlayerCtrl.Ins.PlayerSkillSO.LevelSkill;
        _maxLevel = PlayerCtrl.Ins.PlayerSkillSO.MaxLevel;

        _playerSkillCtrl.PlayerSkillMultiShot.MultiShotCount = PlayerCtrl.Ins.PlayerSkillSO.MultiShotCount;
        _playerSkillCtrl.PlayerSkillMultiDirection.MultiDirCount = PlayerCtrl.Ins.PlayerSkillSO.MultiDirCount;
        _playerSkillCtrl.PlayerSkillAoeDamage.AoeRange = PlayerCtrl.Ins.PlayerSkillSO.AoeRange;
        _playerSkillCtrl.PlayerSkillLightning.TimeLightning = PlayerCtrl.Ins.PlayerSkillSO.TimeLightning;
        _playerSkillCtrl.PlayerSkillSpinBall.SpinBallCount = PlayerCtrl.Ins.PlayerSkillSO.SpinBallCount;
        _playerSkillCtrl.PlayerSkillFreeze.TimeFreeze = PlayerCtrl.Ins.PlayerSkillSO.TimeFreeze;
        _playerSkillCtrl.PlayerSkillRocket.TimeRoket = PlayerCtrl.Ins.PlayerSkillSO.TimeRoket;
    }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
    }
}
