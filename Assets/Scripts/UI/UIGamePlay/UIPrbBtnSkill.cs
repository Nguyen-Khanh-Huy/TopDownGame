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
    }

    private void TextSkill(TMP_Text txtSkill, PlayerSkillAbstract playerSkill)
    {
        if (playerSkill is PlayerSkillMultiShot)
        {
            if(playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: Double Shot.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: Triple Shot.";
        }

        else if(playerSkill is PlayerSkillMultiDirection)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: Three-Way Bullet.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: Five-Way Bullet.";
        }

        else if (playerSkill is PlayerSkillShootRange)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +10% Attack Range.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +20% Attack Range.";
        }

        else if (playerSkill is PlayerSkillShootSpeed)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +15% Attack Speed.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +15% Attack Speed.";
        }

        else if (playerSkill is PlayerSkillAoeDamage)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +1 AoE Range. All damage will be in the form of AoE damage.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +2 AoE Range. All damage will be in the form of AoE damage.";
        }

        else if (playerSkill is PlayerSkillMoveSpeed)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +15% Move Speed.";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +25% Move Speed.";
        }

        else if (playerSkill is PlayerSkillLightning)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: Lightning strikes all enemies within attack range (7s Cooldown).";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: Lightning strikes all enemies within attack range (5s Cooldown).";
        }

        else if (playerSkill is PlayerSkillSpinBall)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +3 Fireballs. Spin around you and damage Enemies";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +5 Fireballs. Spin around you and damage Enemies";
        }

        else if (playerSkill is PlayerSkillFreeze)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +1s Freeze Time. Freezes all enemies within attack range (6s Cooldown).";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +2s Freeze Time. Freezes all enemies within attack range (6s Cooldown).";
        }

        else if (playerSkill is PlayerSkillRocket)
        {
            if (playerSkill.LevelSkill == 1)
                txtSkill.text = "Level 1: +1 Rocket. Fire at the most crowded enemy area (6s cooldown)";
            else if (playerSkill.LevelSkill == 2)
                txtSkill.text = "Level Max: +1 Rocket. Fire at the most crowded enemy area (4s cooldown)";
        }
    }
}
