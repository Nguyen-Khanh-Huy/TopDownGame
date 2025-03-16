using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSkillAbstract : PISMonoBehaviour
{
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected int _levelSkill;
    [SerializeField] protected int _maxLevel;

    public int LevelSkill { get => _levelSkill; }

    public virtual void Upgrade()
    {
        if (_levelSkill >= _maxLevel) return;
        _levelSkill++;
        // For Override
    }

    public BulletCtrlAbstract GetBullet()
    {
        return _playerCtrl.BulletPlayer; // Get Type Bullet
    }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _levelSkill = _playerCtrl.PlayerSkillSO.LevelSkill;
        _maxLevel = _playerCtrl.PlayerSkillSO.MaxLevel;
        _playerCtrl.PlayerSkillsCtrl.PlayerSkillBulletFiveShots.ShotCount = _playerCtrl.PlayerSkillSO.ThreeTime;
        _playerCtrl.PlayerSkillsCtrl.PlayerSkillBulletTripleBeam.TripleBeam = _playerCtrl.PlayerSkillSO.TripleBeam;
    }
}
