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
                txtSkill.text = "Attack Range: +10%";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Attack Range: +20%";
            }
        }
        else if (playerSkill is PlayerSkillShootSpeed)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "Attack Speed: +15%";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Attack Speed: +25%";
            }
        }
        else if (playerSkill is PlayerSkillAoeDamage)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "+1 AoE Range: All damage will be in the form of AoE damage.";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "+2 AoE Range: All damage will be in the form of AoE damage.";
            }
        }
        else if (playerSkill is PlayerSkillMoveSpeed)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "Move Speed: +15%";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Move Speed: +25%";
            }
        }
        else if (playerSkill is PlayerSkillLightning)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "Lightning strikes all enemies within attack range(5s Cooldown).";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "Lightning strikes all enemies within attack range(2s Cooldown).";
            }
        }
        else if (playerSkill is PlayerSkillSpinBall)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "+3 Fireballs: Spin around you and damage Enemies";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "+5 Fireballs: Spin around you and damage Enemies";
            }
        }
        else if (playerSkill is PlayerSkillFreeze)
        {
            if (playerSkill.LevelSkill == 1)
            {
                txtSkill.text = "+1s Freeze Time: Freezes all enemies within attack range(5s Cooldown).";
            }
            else if (playerSkill.LevelSkill == 2)
            {
                txtSkill.text = "+2s Freeze Time: Freezes all enemies within attack range(5s Cooldown).";
            }
        }
        //else if (playerSkill is PlayerSkillFreeze)
        //{
        //    if (playerSkill.LevelSkill == 1)
        //    {
        //        txtSkill.text = "Fire 3 missiles: To random enemies within attack range(3s Cooldown).";
        //    }
        //    else if (playerSkill.LevelSkill == 2)
        //    {
        //        txtSkill.text = "Fire 5 missiles: To random enemies within attack range(3s Cooldown).";
        //    }
        //}
    }
}
