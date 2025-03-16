using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        Invoke(nameof(FireBullet), _playerCtrl.FireSpeed);
    }

    private void FireBullet()
    {
        Invoke(nameof(FireBullet), _playerCtrl.FireSpeed);
        if (_playerCtrl.PlayerTarget.Target == null) return;
        //PoolManager<BulletCtrlAbstract>.Ins.Spawn(_playerCtrl.BulletPlayer, _playerCtrl.FirePoint.position, _playerCtrl.transform.rotation);
        _playerCtrl.PlayerSkillsCtrl.PlayerSkillMain.SkillBulletMain();
    }

    public void PlayerTakeDamage()
    {
        if (_playerCtrl.Hp <= 0) return;
        _playerCtrl.Hp--;
    }
}
