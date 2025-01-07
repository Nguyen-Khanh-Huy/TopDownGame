using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : PISMonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        transform.SetPositionAndRotation(new Vector3(_player.transform.position.x, _player.transform.position.y + 10f, _player.transform.position.z - 5f), 
            Quaternion.Euler(60f,0f,0f));
    }
    protected override void LoadComponents()
    {
        if (_player != null) return;
        _player = GameObject.Find("Player");
    }
}
