using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillList : PISMonoBehaviour
{
    [SerializeField] private PlayerSkillsCtrl _playerSkillCtrl;
    [SerializeField] private PlayerSkillMoveSpeed _playerSkillMoveSpeed;
    [SerializeField] private PlayerSkillShootRange _playerSkillShootRange;
    [SerializeField] private PlayerSkillShootSpeed _playerSkillShootSpeed;
    [SerializeField] private PlayerSkillMultiShot _playerSkillMultiShot;
    [SerializeField] private PlayerSkillMultiDirection _playerSkillMultiDirection;
    [SerializeField] private PlayerSkillAoeDamage _playerSkillAoeDamage;
    [SerializeField] private PlayerSkillLightning _playerSkillLightning;
    [SerializeField] private PlayerSkillSpinBall _playerSkillSpinBall;
    [SerializeField] private PlayerSkillFreeze _playerSkillFreeze;

    [SerializeField] private List<PlayerSkillAbstract> _listAllPbSkills = new();

    public PlayerSkillMoveSpeed PlayerSkillMoveSpeed { get => _playerSkillMoveSpeed; }
    public PlayerSkillShootRange PlayerSkillShootRange { get => _playerSkillShootRange; }
    public PlayerSkillShootSpeed PlayerSkillShootSpeed { get => _playerSkillShootSpeed; }
    public PlayerSkillMultiShot PlayerSkillMultiShot { get => _playerSkillMultiShot; }
    public PlayerSkillMultiDirection PlayerSkillMultiDirection { get => _playerSkillMultiDirection; }
    public PlayerSkillAoeDamage PlayerSkillAoeDamage { get => _playerSkillAoeDamage; }
    public PlayerSkillLightning PlayerSkillLightning { get => _playerSkillLightning; }
    public PlayerSkillSpinBall PlayerSkillSpinBall { get => _playerSkillSpinBall; }
    public PlayerSkillFreeze PlayerSkillFreeze { get => _playerSkillFreeze; }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null  && _playerSkillMoveSpeed != null && _playerSkillShootRange != null && _playerSkillShootSpeed != null && _playerSkillMultiShot != null && _playerSkillMultiDirection != null && _playerSkillAoeDamage != null && _playerSkillLightning != null && _playerSkillSpinBall != null && _playerSkillFreeze != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
        _playerSkillMoveSpeed = GetComponentInChildren<PlayerSkillMoveSpeed>();
        _playerSkillShootRange = GetComponentInChildren<PlayerSkillShootRange>();
        _playerSkillShootSpeed = GetComponentInChildren<PlayerSkillShootSpeed>();
        _playerSkillMultiShot = GetComponentInChildren<PlayerSkillMultiShot>();
        _playerSkillMultiDirection = GetComponentInChildren<PlayerSkillMultiDirection>();
        _playerSkillAoeDamage = GetComponentInChildren<PlayerSkillAoeDamage>();
        _playerSkillLightning = GetComponentInChildren<PlayerSkillLightning>();
        _playerSkillSpinBall = GetComponentInChildren<PlayerSkillSpinBall>();
        _playerSkillFreeze = GetComponentInChildren<PlayerSkillFreeze>();

        if (_listAllPbSkills.Count == transform.childCount) return;
        _listAllPbSkills.Clear();
        foreach (Transform child in transform)
        {
            PlayerSkillAbstract skill = child.GetComponent<PlayerSkillAbstract>();
            if (skill != null)
            {
                _listAllPbSkills.Add(skill);
            }
        }
    }

    public PlayerSkillAbstract GetRandomSkill()
    {
        List<PlayerSkillAbstract> SelectedSkills = _listAllPbSkills.Where(skill => skill.LevelSkill > 0 && skill.LevelSkill < 3).ToList();
        if (SelectedSkills.Count == 0)
            return null;

        int rand = Random.Range(0, SelectedSkills.Count);
        return SelectedSkills[rand];
    }

    public BulletCtrlAbstract GetBullet()
    {
        return _playerSkillCtrl.PlayerCtrl.BulletPlayer; // Get Type Bullet
    }
}
