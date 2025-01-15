using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDropCtrlAbstract : PoolObj<ItemDropCtrlAbstract>
{
    [SerializeField] private Rigidbody _rb;

    private void Start()
    {
        ItemFalling();
    }

    private void OnEnable()
    {
        Invoke(nameof(DespawnItem), 5f);
    }

    private void DespawnItem()
    {
        PoolManager<ItemDropCtrlAbstract>.Ins.Despawn(this);
    }

    private void ItemFalling()
    {
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        _rb.AddForce(randomDirection * 3f, ForceMode.Impulse);
    }

    protected override void LoadComponents()
    {
        if (_rb != null) return;
        _rb = GetComponent<Rigidbody>();
    }
}
