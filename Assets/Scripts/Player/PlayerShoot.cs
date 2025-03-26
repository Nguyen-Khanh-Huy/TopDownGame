using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _countEnemyDead;

    public float ShootSpeed { get => _shootSpeed; set => _shootSpeed = value; }
    public int CountEnemyDead { get => _countEnemyDead; set => _countEnemyDead = value; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        //Invoke(nameof(FireBullet), _shootSpeed);
        FireBullet();
    }

    private void FireBullet()
    {
        Invoke(nameof(FireBullet), _shootSpeed);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (_playerCtrl.PlayerTarget.Target == null) return;

        _playerCtrl.PlayerSkillsCtrl.PlayerSkillMain.SkillBulletMain();
    }
}
