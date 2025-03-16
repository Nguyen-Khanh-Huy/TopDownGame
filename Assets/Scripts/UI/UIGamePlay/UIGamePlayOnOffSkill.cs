using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlayOnOffSkill : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private UIPrbBtnSkill _uiPrbBtnSkill;
    [SerializeField] private Transform _panelListSkills;
    [SerializeField] private List<PlayerSkillAbstract> _listRandomSkills = new();
    [SerializeField] private List<UIPrbBtnSkill> _listSelectSkills = new();
    private int skillCount = 0;

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _uiPrbBtnSkill != null && _panelListSkills != null) return;
        _playerCtrl = FindObjectOfType<PlayerController>();
        _uiPrbBtnSkill = Resources.Load<UIPrbBtnSkill>("UI/UIPrbBtnSkill");
        _panelListSkills = transform.Find("PanelListSkills").GetComponent<Transform>();
    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void OnEnable()
    {
        ShowListSkill();
    }

    private void OnDisable()
    {
        HideListSkill();
    }

    private void Init()
    {
        if (_listSelectSkills.Count > 0) return;
        foreach (Transform child in _panelListSkills)
        {
            UIPrbBtnSkill skillButton = child.GetComponent<UIPrbBtnSkill>();
            if (skillButton != null)
            {
                _listSelectSkills.Add(skillButton);
            }
        }
    }

    private void ShowListSkill()
    {
        RandomSkill();
        for (int i = 0; i < _listSelectSkills.Count; i++)
        {
            _uiPrbBtnSkill.SetLevelItem(_listSelectSkills[i], _listRandomSkills[i]);
            _listSelectSkills[i].gameObject.SetActive(true);
        }
    }

    private void RandomSkill()
    {
        _listRandomSkills.Clear();
        skillCount = 0;

        for (int i = 0; i < Mathf.Infinity && skillCount < 3; i++)
        {
            PlayerSkillAbstract playerSkill = _playerCtrl.PlayerSkillsCtrl.GetRandomSkill();
            if (playerSkill != null && !_listRandomSkills.Contains(playerSkill))
            {
                _listRandomSkills.Add(playerSkill);
                skillCount++;
            }
        }
    }

    private void HideListSkill()
    {
        if (_listSelectSkills.Count == 0) return;
        foreach (UIPrbBtnSkill skillButton in _listSelectSkills)
        {
            skillButton.gameObject.SetActive(false);
        }
    }
}
