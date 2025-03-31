using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFlashingEffect : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] private Renderer[] _renderer;
    [SerializeField] private MaterialPropertyBlock _propertyBlock;

    [SerializeField] private float _timeFlashing = 0.5f;
    [SerializeField] private float _timeInterval = 0.1f;

    private Coroutine _flashCoroutine;

    public void StartFlash()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            ResetColor();
        }
        _flashCoroutine = StartCoroutine(Flashing());
    }

    private IEnumerator Flashing()
    {
        float timeCount = 0f;
        bool toggle = true;

        while (timeCount < _timeFlashing)
        {
            if (_enemyCtrl.Hp <= 0)
            {
                ResetColor();
                yield break;
            }

            SetColor(toggle ? Color.red : Color.white);
            toggle = !toggle;

            yield return new WaitForSeconds(_timeInterval);
            timeCount += _timeInterval;
        }

        ResetColor();
    }

    private void ApplyColor(Color color)
    {
        _propertyBlock ??= new MaterialPropertyBlock();

        foreach (var renderer in _renderer)
        {
            if (renderer == null) continue;

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(_propertyBlock);
        }
    }

    private void SetColor(Color color) => ApplyColor(color);
    private void ResetColor() => ApplyColor(Color.white);

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null && _renderer.Length > 0) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
        //_renderer = transform.parent.GetComponentInChildren<Renderer>();
        //_defaultColor = _renderer.sharedMaterial.color;
        _renderer = transform.parent.Find("Model").GetComponentsInChildren<Renderer>();
    }
}
