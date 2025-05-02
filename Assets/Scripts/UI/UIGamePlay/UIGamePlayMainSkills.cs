using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlayMainSkills : PISMonoBehaviour
{
    [SerializeField] private UIPrbBtnSkill _uiPrbBtnSkill;
    [SerializeField] private Transform _panelListSkills;
    [SerializeField] private List<UIPrbBtnSkill> _listDefaultSkills = new();
    [SerializeField] private List<PlayerSkillAbstract> _listRandomSkills = new();
    private int skillCount = 0;

    protected override void LoadComponents()
    {
        if (_uiPrbBtnSkill != null && _panelListSkills != null && _listDefaultSkills.Count > 0) return;
        _uiPrbBtnSkill = Resources.Load<UIPrbBtnSkill>("UI/UIPrbBtnSkill");
        _panelListSkills = transform.Find("PanelListSkills").GetComponent<Transform>();
        
        foreach (Transform child in _panelListSkills)
        {
            if (child.TryGetComponent<UIPrbBtnSkill>(out var skillButton))
            {
                _listDefaultSkills.Add(skillButton);
            }
        }
    }

    private void OnEnable()
    {
        UIGamePlayManager.Ins.CheckPlayTime = false;
        ShowListSkill();
    }

    private void OnDisable()
    {
        UIGamePlayManager.Ins.CheckPlayTime = true;
    }

    private void ShowListSkill()
    {
        RandomSkill();
        for (int i = 0; i < _listDefaultSkills.Count; i++)
        {
            _uiPrbBtnSkill.SetLevelItem(_listDefaultSkills[i], _listRandomSkills[i]);
        }
    }

    private void RandomSkill()
    {
        _listRandomSkills.Clear();
        skillCount = 0;

        for (int i = 0; i < Mathf.Infinity && skillCount < 3; i++)
        {
            PlayerSkillAbstract playerSkill = PlayerCtrl.Ins.PlayerSkillsCtrl.GetRandomSkill();
            if (playerSkill != null && !_listRandomSkills.Contains(playerSkill))
            {
                _listRandomSkills.Add(playerSkill);
                skillCount++;
            }
        }
    }

}
