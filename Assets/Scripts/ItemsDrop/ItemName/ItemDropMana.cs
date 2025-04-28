using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDropMana : ItemDropCtrlAbstract
{
    [SerializeField] private PlayerController _playerCtrl;
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
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            _playerCtrl.PlayerMana.AddMana();
            UIGamePlayManager.Ins.Slider.value = (float)_playerCtrl.PlayerMana.CurMana / _playerCtrl.PlayerMana.ManaNextLevel;
            DespawnItem();
        }
    }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _col != null) return;
        _playerCtrl = FindObjectOfType<PlayerController>();
        _col = GetComponent<SphereCollider>();
    }
    public override string GetName()
    {
        return "Mana";
    }
}
