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
    }

    private void TextSkill(TMP_Text txtSkill, PlayerSkillAbstract playerSkill)
    {
        if (playerSkill is PlayerSkillFiveShots)
        {
            if(playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "PlayerSkill Five Shots LV1";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "PlayerSkill Five Shots LV2";
            }
        }
        else if(playerSkill is PlayerSkillMultiDirection)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "PlayerSkill Multi Direction LV1";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "PlayerSkill Multi Direction LV2";
            }
        }
        else if (playerSkill is PlayerSkillShootRange)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "PlayerSkill Shoot Range LV1";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "PlayerSkill Shoot Range LV2";
            }
        }
        else if (playerSkill is PlayerSkillShootSpeed)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "PlayerSkill Shoot Speed LV1";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "PlayerSkill Shoot Speed LV2";
            }
        }
    }
}
