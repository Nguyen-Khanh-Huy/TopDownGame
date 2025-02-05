using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : PISMonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    protected override void Awake()
    {
        _canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }

    protected override void LoadComponents()
    {
        if (_canvas != null) return;
        _canvas = GetComponent<Canvas>();
    }
}
