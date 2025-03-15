using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPrbBtnSkill : PISMonoBehaviour
{
    public Button BtnSkill;
    public TMP_Text TxtBtnSkill;
    protected override void LoadComponents()
    {
        if (BtnSkill != null && TxtBtnSkill != null) return;
        BtnSkill = GetComponent<Button>();
        TxtBtnSkill = GetComponentInChildren<TMP_Text>();
    }

    public void SetLevelItem(UIPrbBtnSkill uiPrbSkill, PlayerSkillAbstract playerSkill)
    {
        uiPrbSkill.TxtBtnSkill.text = playerSkill.GetType().Name;
        uiPrbSkill.BtnSkill.onClick.RemoveAllListeners();
        uiPrbSkill.BtnSkill.onClick.AddListener(() => playerSkill.LaunchSkill());
    }
}
