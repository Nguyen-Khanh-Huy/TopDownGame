using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillsCtrl : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private PlayerSkillMain _playerSkillMain;
    [SerializeField] private PlayerSkillList _playerSkillList;

    public PlayerController PlayerCtrl { get => _playerCtrl; }
    public PlayerSkillMain PlayerSkillMain { get => _playerSkillMain; }
    public PlayerSkillList PlayerSkillList { get => _playerSkillList; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _playerSkillMain != null && _playerSkillList != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
        _playerSkillMain = GetComponentInChildren<PlayerSkillMain>();
        _playerSkillList = GetComponentInChildren<PlayerSkillList>();
    }
}
