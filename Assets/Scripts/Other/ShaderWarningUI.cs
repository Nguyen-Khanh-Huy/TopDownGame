using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderWarningUI : PISMonoBehaviour
{
    [SerializeField] private Material _warningMaterial;
    [SerializeField] private int _repeatCount = 3;
    [SerializeField] private float _delayTime = 1.5f;

    [SerializeField] private float timer = 0f;
    [SerializeField] private float _phase = 0f;
    [SerializeField] private int _curRepeat = 0;
    [SerializeField] private bool _isWarning;

    public bool IsWarning { get => _isWarning; set => _isWarning = value; }

    private void OnEnable()
    {
        _warningMaterial.SetFloat("_Phase", 0f);
    }

    private void Update()
    {
        if (_isWarning)
        {
            timer += Time.deltaTime;
            _phase = Mathf.PingPong(timer / _delayTime, 1f);
            _warningMaterial.SetFloat("_Phase", _phase);

            if (timer >= _delayTime)
            {
                _curRepeat++;
                timer = 0f;
            }

            if (_curRepeat >= _repeatCount)
            {
                _phase = 0f;
                _curRepeat = 0;
                _isWarning = false;
            }
        }
    }

    protected override void LoadComponents()
    {
        if (_warningMaterial != null) return;
        _warningMaterial = Resources.Load<Material>("Shader/WarningMaterial");
    }
}