using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropMana : ItemDropCtrlAbstract
{
    [SerializeField] private SphereCollider _col;
    private Vector3 _targetPos;
    private float _moveSpeed = 3f;

    private void OnEnable()
    {
        float randomX = transform.position.x + Random.Range(-1f, 1f);
        float randomZ = transform.position.z + Random.Range(-1f, 1f);
        _targetPos = new Vector3(randomX, 0.2f, randomZ);
    }

    private void Update()
    {
        if (transform.position.y <= 0.2f) return;
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerCtrl.Ins.PlayerMana.AddMana();
            UIGamePlayManager.Ins.SliderMana.value = (float)PlayerCtrl.Ins.PlayerMana.CurMana / PlayerCtrl.Ins.PlayerMana.ManaNextLevel;
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
