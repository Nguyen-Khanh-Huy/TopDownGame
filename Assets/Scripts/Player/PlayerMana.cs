using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private int _curMana = 0;
    [SerializeField] private int _curLevel = 1;
    [SerializeField] private int _manaNextLevel = 10;

    public int CurMana { get => _curMana; set => _curMana = value; }
    public int CurLevel { get => _curLevel; set => _curLevel = value; }
    public int ManaNextLevel { get => _manaNextLevel; set => _manaNextLevel = value; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    public void AddMana()
    {
        if (_curLevel < 1 || _curLevel >= 6) return;
        _curMana++;
        if (_curMana >= _manaNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _curLevel++;
        _curMana = _playerCtrl.PlayerSO.CurMana;
        int SO = _playerCtrl.PlayerSO.ManaNextLevel;    // SO = 10
        switch (_curLevel)
        {
            case 2:
                _manaNextLevel += SO * 1;
                break;
            case 3:
                _manaNextLevel += SO * 2;
                break;
        }

        UIGamePlayManager.Ins.Show(UIGamePlayManager.Ins.PanelSkillsDialog);
    }
}
