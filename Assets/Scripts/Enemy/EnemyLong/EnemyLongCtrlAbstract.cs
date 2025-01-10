using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLongCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyLongAttack _enemyLongAttack;
    [SerializeField] private BulletParabol _bulletParabol;
    [SerializeField] private Transform _firePoint;
    public EnemyLongAttack EnemyLongAttack { get => _enemyLongAttack; set => _enemyLongAttack = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemyLongAttack != null && _bulletParabol != null && _firePoint != null) return;
        _enemyLongAttack = GetComponentInChildren<EnemyLongAttack>();
        _bulletParabol = Resources.Load<BulletParabol>("Bullets/BulletParabol");
        _firePoint = transform.Find("Model/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/FirePoint");
    }

    private void OnEnable()
    {
        _hp = 3;
    }

    public void EventFireBulletParabol()
    {
        Instantiate(_bulletParabol, _firePoint.position, Quaternion.identity);
    }
}
