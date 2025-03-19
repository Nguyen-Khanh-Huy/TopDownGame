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
        TextSkill(uiPrbSkill.TxtBtnSkill, playerSkill);
        uiPrbSkill.BtnSkill.onClick.RemoveAllListeners();
        uiPrbSkill.BtnSkill.onClick.AddListener(() => BtnAction(playerSkill));
    }

    private void BtnAction(PlayerSkillAbstract playerSkill)
    {
        playerSkill.Upgrade();
        UIGamePlayManager.Ins.Close(UIGamePlayManager.Ins.PanelSkillsDialog);
        UIGamePlayManager.Ins.CheckPlayTime = true;
    }

    private void TextSkill(TMP_Text txtSkill, PlayerSkillAbstract playerSkill)
    {
        if (playerSkill is PlayerSkillMultiShot)
        {
            if(playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "Double Shot";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Triple Shot";
            }
        }
        else if(playerSkill is PlayerSkillMultiDirection)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "Three-Way Bullet";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Five-Way Bullet";
            }
        }
        else if (playerSkill is PlayerSkillShootRange)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "+10% Range";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "+20% Range";
            }
        }
        else if (playerSkill is PlayerSkillShootSpeed)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "+15% Fire Rate";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "+25% Fire Rate";
            }
        }
    }
}
