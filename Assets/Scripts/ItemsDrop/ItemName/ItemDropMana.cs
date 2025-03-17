using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDropMana : ItemDropCtrlAbstract
{
    [SerializeField] private SphereCollider _col;
    private Vector3 _fallPosition;
    private float _fallSpeed = 3f;

    private void OnEnable()
    {
        float randomX = transform.position.x + Random.Range(-1f, 1f);
        float randomZ = transform.position.z + Random.Range(-1f, 1f);
        _fallPosition = new Vector3(randomX, 0.2f, randomZ);
    }

    private void Update()
    {
        if (transform.position.y <= 0.2f) return;
        transform.position = Vector3.MoveTowards(transform.position, _fallPosition, _fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            DespawnItem();
        }
    }

    protected override void LoadComponents()
    {
        if (_col != null) return;
        _col = GetComponent<SphereCollider>();
    }
    public override string GetName()
    {
        return "Mana";
    }
}
