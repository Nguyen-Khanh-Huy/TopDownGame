using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private float _fireSpeed = 0.5f;

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        Invoke(nameof(FireBullet), _fireSpeed);
    }

    private void FireBullet()
    {
        Invoke(nameof(FireBullet), _fireSpeed);
        if (_playerCtrl.PlayerTarget.Target == null) return;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_playerCtrl.BulletPlayer, _playerCtrl.FirePoint.position, _playerCtrl.transform.rotation);
    }
}
