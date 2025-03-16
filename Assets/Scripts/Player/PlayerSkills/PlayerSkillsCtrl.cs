using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillsCtrl : PISMonoBehaviour
{
    [SerializeField] private List<PlayerSkillAbstract> _listAllPbSkills = new();

    [SerializeField] private PlayerSkillMain _playerSkillMain;
    [SerializeField] private PlayerSkillFiveShots _playerSkillBulletFiveShots;
    [SerializeField] private PlayerSkillMultiDirection _playerSkillMultiDirection;
    [SerializeField] private PlayerSkillAttackRange _playerSkillAttackRange;
    [SerializeField] private PlayerSkillAttackSpeed _playerSkillAttackSpeed;

    public PlayerSkillMain PlayerSkillMain { get => _playerSkillMain; }

    public PlayerSkillFiveShots PlayerSkillBulletFiveShots { get => _playerSkillBulletFiveShots; }
    public PlayerSkillMultiDirection PlayerSkillBulletTripleBeam { get => _playerSkillMultiDirection; }
    public PlayerSkillAttackRange PlayerSkillIndexAttackRange { get => _playerSkillAttackRange; }
    public PlayerSkillAttackSpeed PlayerSkillIndexAttackSpeed { get => _playerSkillAttackSpeed; }

    protected override void LoadComponents()
    {
        if (_playerSkillMain != null && _playerSkillBulletFiveShots != null && _playerSkillMultiDirection != null && _playerSkillAttackRange != null && _playerSkillAttackSpeed != null) return;
        _playerSkillMain = GetComponentInChildren<PlayerSkillMain>();
        _playerSkillBulletFiveShots = GetComponentInChildren<PlayerSkillFiveShots>();
        _playerSkillMultiDirection = GetComponentInChildren<PlayerSkillMultiDirection>();
        _playerSkillAttackRange = GetComponentInChildren<PlayerSkillAttackRange>();
        _playerSkillAttackSpeed = GetComponentInChildren<PlayerSkillAttackSpeed>();

        if (_listAllPbSkills.Count == transform.childCount) return;
        foreach (Transform child in transform)
        {
            PlayerSkillAbstract skill = child.GetComponent<PlayerSkillAbstract>();
            if (skill != null && !(skill is PlayerSkillMain))
            {
                _listAllPbSkills.Add(skill);
            }
        }
    }

    public PlayerSkillAbstract GetRandomSkill()
    {
        List<PlayerSkillAbstract> filteredSkills = _listAllPbSkills.Where(skill => skill.LevelSkill > 0 && skill.LevelSkill < 3 && !(skill is PlayerSkillMain)).ToList();
        if (filteredSkills.Count == 0)
            return null;

        int rand = Random.Range(0, filteredSkills.Count);
        return filteredSkills[rand];
    }
}
