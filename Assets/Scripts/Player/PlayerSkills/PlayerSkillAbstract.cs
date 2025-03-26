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
        if (_levelSkill >= _maxLevel) return;
        _levelSkill++;
        // For Override
    }

    private void Init()
    {
        _levelSkill = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.LevelSkill;
        _maxLevel = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MaxLevel;
        _playerSkillCtrl.PlayerSkillList.PlayerSkillMultiShot.MultiShotCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MultiShotCount;
        _playerSkillCtrl.PlayerSkillList.PlayerSkillMultiDirection.MultiDirCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.MultiDirCount;
        _playerSkillCtrl.PlayerSkillList.PlayerSkillAoeBullet.AoeBullet = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.AoeBullet;
        _playerSkillCtrl.PlayerSkillList.PlayerSkillLightning.TimeLightning = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.TimeLightning;
        _playerSkillCtrl.PlayerSkillList.PlayerSkillSpinBall.SpinBallCount = _playerSkillCtrl.PlayerCtrl.PlayerSkillSO.SpinBallCount;
    }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
    }
}
